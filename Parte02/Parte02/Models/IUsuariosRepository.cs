using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Parte02.Models
{
    public interface IUsuariosRepository
    {
        Usuario login(string usuario, string contrasenia);
    }
}
