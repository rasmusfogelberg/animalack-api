namespace AnimalackApi.Helpers.JWT;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimalackApi.Entities;
using Microsoft.IdentityModel.Tokens;

public interface IJWTUtils
{
  public string GenerateToken(User user);
  public int? ValidateToken(string token);
}
public class JWTUtils : IJWTUtils
{
  private readonly DataContext _context;
  public JWTUtils(DataContext context)
  {
    _context = context;
  }
  public string GenerateToken(User user)
  {
    var handler = new JwtSecurityTokenHandler();

    var key = Encoding.ASCII.GetBytes("IamASuperSecretString");
    var tokenDescription = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
      Expires = DateTime.UtcNow.AddMinutes(15),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = handler.CreateToken(tokenDescription);

    return handler.WriteToken(token);
  }

  public int? ValidateToken(string token)
  {
    if (token == null) return null;

    var handler = new JwtSecurityTokenHandler();
    
    var key = Encoding.ASCII.GetBytes("IamASuperSecretString");
    try
    {
      handler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwt = (JwtSecurityToken)validatedToken;
      var userId = int.Parse(jwt.Claims.First(claim => claim.Type == "id").Value);

      return userId;
    }
    catch
    {
      return null;
    }
  }
}