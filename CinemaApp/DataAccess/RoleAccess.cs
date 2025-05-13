using Dapper;

public static class RoleAccess
{
    public static Role GetRoleById(int roleId)
    {
        using (var connection = Db.CreateConnection())
        {
            string sql = @"
                SELECT 
                    Id,
                    Name,
                    manage_food_menu AS ManageFoodMenu,
                    manage_accounts AS ManageAccounts,
                    manage_guest_accounts AS ManageGuestAccounts,
                    manage_movie_sessions AS ManageMovieSessions,
                    manage_movie_hall AS ManageMovieHall,
                    manage_reservations AS ManageReservations
                FROM role
                WHERE Id = @RoleId";
            return connection.QueryFirstOrDefault<Role>(sql, new { RoleId = roleId });
        }
    }
}
