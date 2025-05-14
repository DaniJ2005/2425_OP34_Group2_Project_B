using System;
using System.Collections.Generic;
using System.Linq;

public class ReservationManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "Reservation Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "View All Reservations", "Find Reservation by ID", "Find Reservations by Email", 
                             "Find Reservations by Movie Session", "Update Reservation Status", "Cancel Reservation", "Back" };
        
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║       RESERVATION MANAGEMENT         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("[↑][↓] to navigate, [ENTER] to select, [ESC] to go back\n");
            
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($" {options[i]}");
                }
            }
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;
            
            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        ViewAllReservations();
                        break;
                    case 1:
                        FindReservationById();
                        break;
                    case 2:
                        FindReservationsByEmail();
                        break;
                    case 3:
                        FindReservationsByMovieSession();
                        break;
                    case 4:
                        UpdateReservationStatus();
                        break;
                    case 5:
                        CancelReservation();
                        break;
                    case 6:
                        MenuLogic.NavigateToPrevious();
                        LoggerLogic.Instance.Log("User returned to admin menu from reservation management");
                        return;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to admin menu");
                return;
            }
        } while (true);
    }

    private void ViewAllReservations()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           ALL RESERVATIONS           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            var reservations = ReservationAdminLogic.GetAllReservations();
            
            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }
            
            DisplayReservationsList(reservations);
            
            // Ask if user wants to see details of any reservation
            Console.Write("\nEnter a reservation ID to view details (or press Enter to go back): ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int reservationId))
            {
                DisplayReservationDetails(reservationId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in ViewAllReservations: {ex.Message}");
        }
    }

    private void FindReservationById()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║        FIND RESERVATION BY ID        ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            Console.Write("Enter Reservation ID: ");
            if (!int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.WriteLine("Invalid Reservation ID!");
                return;
            }
            
            var reservation = ReservationAdminLogic.GetReservationById(reservationId);
            
            if (reservation == null)
            {
                Console.WriteLine("Reservation not found!");
                return;
            }
            
            DisplayReservationDetails(reservationId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in FindReservationById: {ex.Message}");
        }
    }

    private void FindReservationsByEmail()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║     FIND RESERVATIONS BY EMAIL       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty!");
                return;
            }
            
            var reservations = ReservationAdminLogic.GetReservationsByEmail(email);
            
            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for this email address.");
                return;
            }
            
            DisplayReservationsList(reservations);
            
            // Ask if user wants to see details of any reservation
            Console.Write("\nEnter a reservation ID to view details (or press Enter to go back): ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int reservationId))
            {
                DisplayReservationDetails(reservationId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in FindReservationsByEmail: {ex.Message}");
        }
    }

    private void FindReservationsByMovieSession()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║  FIND RESERVATIONS BY MOVIE SESSION  ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            // Display available movie sessions first
            Console.WriteLine("\nAvailable Movie Sessions:");
            var sessions = MovieSessionLogic.GetAllMovieSessions();
            
            if (sessions.Count == 0)
            {
                Console.WriteLine("No movie sessions found.");
                return;
            }
            
            foreach (var session in sessions)
            {
                var movie = MovieLogic.GetMovieById(session.MovieId);
                string movieTitle = movie != null ? movie.Title : "Unknown Movie";
                
                Console.WriteLine($"ID: {session.Id} - {movieTitle} at {session.StartTime} in Hall {session.MovieHallId}");
            }
            
            Console.Write("\nEnter Movie Session ID: ");
            if (!int.TryParse(Console.ReadLine(), out int movieSessionId))
            {
                Console.WriteLine("Invalid Movie Session ID!");
                return;
            }
            
            var reservations = ReservationAdminLogic.GetReservationsByMovieSessionId(movieSessionId);
            
            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for this movie session.");
                return;
            }
            
            DisplayReservationsList(reservations);
            
            // Ask if user wants to see details of any reservation
            Console.Write("\nEnter a reservation ID to view details (or press Enter to go back): ");
            string input = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int reservationId))
            {
                DisplayReservationDetails(reservationId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in FindReservationsByMovieSession: {ex.Message}");
        }
    }

    private void UpdateReservationStatus()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║       UPDATE RESERVATION STATUS      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            Console.Write("Enter Reservation ID: ");
            if (!int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.WriteLine("Invalid Reservation ID!");
                return;
            }
            
            var reservation = ReservationAdminLogic.GetReservationById(reservationId);
            
            if (reservation == null)
            {
                Console.WriteLine("Reservation not found!");
                return;
            }
            
            Console.WriteLine($"\nCurrent Reservation Status: {reservation.Status}");
            Console.WriteLine("\nAvailable Statuses:");
            Console.WriteLine("1 - Pending");
            Console.WriteLine("2 - Confirmed");
            Console.WriteLine("3 - Completed");
            Console.WriteLine("4 - Canceled");
            
            Console.Write("\nSelect new status (1-4): ");
            if (!int.TryParse(Console.ReadLine(), out int statusChoice) || statusChoice < 1 || statusChoice > 4)
            {
                Console.WriteLine("Invalid status selection!");
                return;
            }
            
            string newStatus;
            switch (statusChoice)
            {
                case 1: newStatus = "Pending"; break;
                case 2: newStatus = "Confirmed"; break;
                case 3: newStatus = "Completed"; break;
                case 4: newStatus = "Canceled"; break;
                default: newStatus = "Pending"; break;
            }
            
            if (ReservationAdminLogic.UpdateReservationStatus(reservationId, newStatus))
            {
                Console.WriteLine($"\nReservation status updated to '{newStatus}' successfully!");
            }
            else
            {
                Console.WriteLine("\nFailed to update reservation status. The requested status change might not be valid.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in UpdateReservationStatus: {ex.Message}");
        }
    }

    private void CancelReservation()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         CANCEL RESERVATION           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        try
        {
            Console.Write("Enter Reservation ID: ");
            if (!int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.WriteLine("Invalid Reservation ID!");
                return;
            }
            
            var reservation = ReservationAdminLogic.GetReservationById(reservationId);
            
            if (reservation == null)
            {
                Console.WriteLine("Reservation not found!");
                return;
            }
            
            Console.WriteLine($"\nReservation Details:");
            Console.WriteLine($"ID: {reservation.Id}");
            Console.WriteLine($"Movie Session ID: {reservation.MovieSessionId}");
            Console.WriteLine($"Current Status: {reservation.Status}");
            Console.WriteLine($"Created At: {reservation.CreatedAt}");
            
            Console.Write("\nAre you sure you want to cancel this reservation? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y")
            {
                Console.WriteLine("Cancellation aborted.");
                return;
            }
            
            if (ReservationAdminLogic.CancelReservation(reservationId))
            {
                Console.WriteLine("\nReservation canceled successfully!");
            }
            else
            {
                Console.WriteLine("\nFailed to cancel reservation. It might be already completed or canceled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            LoggerLogic.Instance.Log($"Error in CancelReservation: {ex.Message}");
        }
    }

    // Helper methods
    private void DisplayReservationsList(List<Reservation> reservations)
    {
        Console.WriteLine("\n=== Reservation List ===");
        Console.WriteLine("ID\tMovie Session\tStatus\t\tCreated At");
        Console.WriteLine("-------------------------------------------------------");
        
        foreach (var reservation in reservations)
        {
            var session = MovieSessionLogic.GetMovieSessionById(reservation.MovieSessionId);
            string sessionInfo = session != null ? GetSessionSummary(session) : "Unknown Session";
            
            Console.WriteLine($"{reservation.Id}\t{sessionInfo}\t{reservation.Status}\t{reservation.CreatedAt}");
        }
        
        Console.WriteLine($"\nTotal Reservations: {reservations.Count}");
    }

    private void DisplayReservationDetails(int reservationId)
    {
        var reservation = ReservationAdminLogic.GetReservationById(reservationId);
        if (reservation == null)
        {
            Console.WriteLine("Reservation not found!");
            return;
        }
        
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║        RESERVATION DETAILS           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        
        // Get movie session details
        var session = MovieSessionLogic.GetMovieSessionById(reservation.MovieSessionId);
        var movie = session != null ? MovieLogic.GetMovieById(session.MovieId) : null;
        
        Console.WriteLine($"\nReservation ID: {reservation.Id}");
        Console.WriteLine($"Status: {reservation.Status}");
        Console.WriteLine($"Created: {reservation.CreatedAt}");
        
        if (session != null)
        {
            Console.WriteLine($"\nMovie Session: {session.Id}");
            Console.WriteLine($"Movie: {(movie != null ? movie.Title : "Unknown")}");
            Console.WriteLine($"Hall: {session.MovieHallId}");
            Console.WriteLine($"Date/Time: {session.StartTime}");
        }
        else
        {
            Console.WriteLine("\nMovie Session: Not Found");
        }
        
        // Get ticket details
        var details = ReservationAdminLogic.GetReservationDetails(reservationId);
        
        if (details.Count > 0)
        {
            Console.WriteLine("\n=== Tickets ===");
            Console.WriteLine("Seat\tType\tPrice");
            Console.WriteLine("------------------");
            
            decimal totalAmount = 0;
            foreach (var detail in details)
            {
                string seatInfo = $"{detail.Row}{detail.Col}";
                string seatType = detail.SeatType.ToString();
                decimal price = detail.Price;
                totalAmount += price;
                
                Console.WriteLine($"{seatInfo}\t{seatType}\t${price:F2}");
            }
            
            Console.WriteLine("------------------");
            Console.WriteLine($"Total: ${totalAmount:F2}");
        }
        else
        {
            Console.WriteLine("\nNo ticket details found for this reservation.");
        }
    }

    private string GetSessionSummary(dynamic session)
    {
        try
        {
            var movie = MoviesessionLogic.GetMovieById(session.MovieId);
            string movieTitle = movie != null ? movie.Title : "Unknown";
            return $"{movieTitle.Substring(0, Math.Min(movieTitle.Length, 15))}...";
        }
        catch
        {
            return "Unknown Session";
        }
    }
}