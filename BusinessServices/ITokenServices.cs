using BusinessEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ITokenServices
    {
        TokenEntity GenerateToken(int userId);

        bool ValidateToken(string tokenId);

        bool kill(string tokenId);

        bool DeleteByUserId(int userId);
    }
}
