using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntites;
using System.Configuration;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    class TokenServices : ITokenServices
    {
        private UnitOfWork _unitOfWork;
        public TokenServices(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(t => t.UserId == userId);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(t => t.UserId == userId).Any();
            return !isNotDeleted;
        }

        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var tokendomain = new Token
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresON = expiredOn
            };
            _unitOfWork.TokenRepository.Insert(tokendomain);
            _unitOfWork.Save();

            var tokenModel = new TokenEntity
            {
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresON = expiredOn,
                AuthToken = token
            };

            return tokenModel;
        }
        public bool ValidateToken(string tokenId)
        {
            var token = _unitOfWork.TokenRepository.Get(t => t.AuthToken == tokenId && t.ExpiresON > DateTime.Now);
            if (token != null && !(DateTime.Now > token.ExpiresON))
            {
                token.ExpiresON = token.ExpiresON.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                _unitOfWork.TokenRepository.Update(token);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool kill(string tokenId)
        {
            _unitOfWork.TokenRepository.Delete(t => t.AuthToken == tokenId);
            _unitOfWork.Save();
            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(t => t.AuthToken == tokenId).Any();
            if (isNotDeleted)
            {
                return false;
            }
            return true;
        }

    }
}
