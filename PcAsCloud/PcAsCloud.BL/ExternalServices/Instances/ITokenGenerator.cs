using PcAsCloud.CORE.Entities;

namespace PcAsCloud.BL.ExternalServices.Instances;
public interface ITokenGenerator
{
    string CreateJWTToken(AppUser user, int hours);
}