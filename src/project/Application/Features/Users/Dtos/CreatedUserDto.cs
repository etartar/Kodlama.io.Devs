﻿using Core.Security.Enums;

namespace Application.Features.Users.Dtos
{
    public class CreatedUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AuthenticatorType AuthenticatorType { get; set; }
    }
}
