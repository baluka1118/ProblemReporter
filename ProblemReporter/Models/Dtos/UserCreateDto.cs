﻿namespace ProblemReporter.Models.Dtos;

public class UserCreateDto
{
    public required string Email { get; set; } = "";

    public required string Password { get; set; } = "";

}