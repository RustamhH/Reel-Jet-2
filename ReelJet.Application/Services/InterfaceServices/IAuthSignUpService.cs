using System;
using System.Linq;
using ReelJet.Database.Entities.Concretes;

namespace Reel_Jet.Services.InterfaceServices {
    public interface IAuthSignUpService {
        bool SignUp(User newUser);
    }
}