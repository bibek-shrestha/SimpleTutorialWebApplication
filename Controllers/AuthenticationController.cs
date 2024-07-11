using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleTutorialWebApplication.Models;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController: ControllerBase
{

    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        var user = ValidateUserCredentials(
            authenticationRequestBody.UserName,
            authenticationRequestBody.Password
        );
        if (user == null) return Unauthorized();
        var securityKey = new SymmetricSecurityKey(
            Convert.FromBase64String(this._configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
        claimsForToken.Add(new Claim("given_name", user.FirstName));
        claimsForToken.Add(new Claim("family_name", user.LastName));
        claimsForToken.Add(new Claim("city", user.City));

        var jwtSecurityToken = new JwtSecurityToken(
            this._configuration["Authentication:Issuer"],
            this._configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return Ok(token);
    }

    private UserInfo ValidateUserCredentials(string? username, string? password)
     {
        return new UserInfo(1, username ?? "", "John", "Doe", "Sydney");

     }

}
