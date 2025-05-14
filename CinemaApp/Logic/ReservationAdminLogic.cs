public static class ReservationAdminLogic

{
    public static List<Reservation> GetAllReservations()
    {
        try
        {
            return ReservationAccess.GetAllReservations();
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving all reservations: {ex.Message}");
            return new List<Reservation>();
        }
    }
    
    public static Reservation GetReservationById(int id)
    {
        try
        {
            return ReservationAccess.GetReservationById(id);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving reservation by ID {id}: {ex.Message}");
            return null;
        }
    }
    
    public static List<Reservation> GetReservationsByEmail(string email)
    {
        try
        {
            return ReservationAccess.GetReservationsByEmail(email);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving reservations for email {email}: {ex.Message}");
            return new List<Reservation>();
        }
    }
    
    public static List<Reservation> GetReservationsByMovieSessionId(int movieSessionId)
    {
        try
        {
            return ReservationAccess.GetReservationsByMovieSessionId(movieSessionId);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving reservations for movie session {movieSessionId}: {ex.Message}");
            return new List<Reservation>();
        }
    }
    
    public static List<Reservation> GetReservationsByStatus(string status)
    {
        try
        {
            return ReservationAccess.GetReservationsByStatus(status);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving reservations with status {status}: {ex.Message}");
            return new List<Reservation>();
        }
    }
    
    public static bool CreateReservation(int movieSessionId, Dictionary<int, int> seatIdToPriceId, string email)
    {
        try
        {
            // Validate movie session exists
            var movieSession = MovieSessionLogic.GetMovieSessionById(movieSessionId);
            if (movieSession == null)
            {
                LoggerLogic.Instance.Log($"Cannot create reservation - Movie Session ID {movieSessionId} not found");
                return false;
            }
            
            // Validate seats and pricing
            Dictionary<Seat, SeatPrice> seats = new Dictionary<Seat, SeatPrice>();
            foreach (var entry in seatIdToPriceId)
            {
                int seatId = entry.Key;
                int seatPriceId = entry.Value;
                
                var seat = SeatAdminLogic.GetSeatById(seatId);
                var seatPrice = GetSeatPriceById(seatPriceId);
                
                if (seat == null)
                {
                    LoggerLogic.Instance.Log($"Cannot create reservation - Seat ID {seatId} not found");
                    return false;
                }
                
                if (seatPrice == null)
                {
                    LoggerLogic.Instance.Log($"Cannot create reservation - Seat Price ID {seatPriceId} not found");
                    return false;
                }
                
                // Check if seat belongs to the movie hall assigned to this session
                if (seat.MovieHallId != movieSession.MovieHallId)
                {
                    LoggerLogic.Instance.Log($"Cannot create reservation - Seat ID {seatId} does not belong to Movie Hall ID {movieSession.MovieHallId}");
                    return false;
                }
                
                // Check if seat is already reserved for this session
                if (IsSeatReservedForSession(seatId, movieSessionId))
                {
                    LoggerLogic.Instance.Log($"Cannot create reservation - Seat ID {seatId} already reserved for Session ID {movieSessionId}");
                    return false;
                }
                
                seats.Add(seat, seatPrice);
            }
            
            // Validate email format
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                LoggerLogic.Instance.Log($"Cannot create reservation - Invalid email format: {email}");
                return false;
            }
            
            // No food items for now
            List<Food> foodItems = new List<Food>();
            
            ReservationAccess.CreateReservation(movieSession, seats, foodItems, email);
            LoggerLogic.Instance.Log($"Reservation created for Session {movieSessionId} with {seats.Count} seats for {email}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error creating reservation: {ex.Message}");
            return false;
        }
    }
    
    public static bool UpdateReservationStatus(int reservationId, string newStatus)
    {
        try
        {
            // Validate reservation exists
            var reservation = GetReservationById(reservationId);
            if (reservation == null)
            {
                LoggerLogic.Instance.Log($"Cannot update reservation - Reservation ID {reservationId} not found");
                return false;
            }
            
            // Validate status
            if (!IsValidStatus(newStatus))
            {
                LoggerLogic.Instance.Log($"Cannot update reservation - Invalid status: {newStatus}");
                return false;
            }
            
            // Check status transition validity
            if (!IsValidStatusTransition(reservation.Status, newStatus))
            {
                LoggerLogic.Instance.Log($"Cannot update reservation - Invalid status transition from {reservation.Status} to {newStatus}");
                return false;
            }
            
            ReservationAccess.UpdateReservationStatus(reservationId, newStatus);
            LoggerLogic.Instance.Log($"Reservation ID {reservationId} status updated from {reservation.Status} to {newStatus}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error updating reservation status: {ex.Message}");
            return false;
        }
    }
    
    public static bool CancelReservation(int reservationId)
    {
        try
        {
            // Validate reservation exists
            var reservation = GetReservationById(reservationId);
            if (reservation == null)
            {
                LoggerLogic.Instance.Log($"Cannot cancel reservation - Reservation ID {reservationId} not found");
                return false;
            }
            
            // Check if reservation can be canceled
            if (reservation.Status == "Completed" || reservation.Status == "Canceled")
            {
                LoggerLogic.Instance.Log($"Cannot cancel reservation - Current status is {reservation.Status}");
                return false;
            }
            
            ReservationAccess.UpdateReservationStatus(reservationId, "Canceled");
            LoggerLogic.Instance.Log($"Reservation ID {reservationId} canceled");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error canceling reservation: {ex.Message}");
            return false;
        }
    }
    
    public static List<dynamic> GetReservationDetails(int reservationId)
    {
        try
        {
            return ReservationAccess.GetReservationDetails(reservationId);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving reservation details for ID {reservationId}: {ex.Message}");
            return new List<dynamic>();
        }
    }
    
    // Helper methods
    private static SeatPrice GetSeatPriceById(int seatPriceId)
    {
        try
        {
            // This would use your actual SeatPrice access layer
            return SeatPriceAccess.GetSeatPriceById(seatPriceId);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error retrieving seat price by ID {seatPriceId}: {ex.Message}");
            return null;
        }
    }
    
    private static bool IsSeatReservedForSession(int seatId, int movieSessionId)
    {
        try
        {
            // This would use your actual reservation system to check if the seat is already reserved
            return ReservationAccess.IsSeatReservedForSession(seatId, movieSessionId);
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error checking if seat {seatId} is reserved for session {movieSessionId}: {ex.Message}");
            return false;
        }
    }
    
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    private static bool IsValidStatus(string status)
    {
        // Define valid status values
        string[] validStatuses = { "Pending", "Confirmed", "Completed", "Canceled" };
        return validStatuses.Contains(status);
    }
    
    private static bool IsValidStatusTransition(string currentStatus, string newStatus)
    {
        // Define valid status transitions
        switch (currentStatus)
        {
            case "Pending":
                return newStatus == "Confirmed" || newStatus == "Canceled";
                
            case "Confirmed":
                return newStatus == "Completed" || newStatus == "Canceled";
                
            case "Completed":
            case "Canceled":
                // Terminal states, no transitions allowed
                return false;
                
            default:
                return false;
        }
    }
}