using System;
using Reel_Jet.Models;
using ReelJet.Database.Entities.Concretes;

namespace Reel_Jet.Services.InterfaceServices {
    public interface IAuthLoginService {
        bool LogIn(User user);
    }
}
