using IronCore.CORE.Entities;

namespace IronCore.BL.ExternalServices.Instances;
public interface ITokenGenerator
{
    string CreateJWTToken(AppUser user, int hours);
}