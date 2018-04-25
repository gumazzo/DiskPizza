﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DiskPizza.Models
{
    public class Usuario : ICustomPrincipal
    {
        public int    Id               { get; set; }
        public string Nome             { get; set; }
        public string Telefone         { get; set; }
        public string Email            { get; set; }
        public string ConfEmail        { get; set; }
        public string Cpf              { get; set; }
        public string Senha            { get; set; }
        public string ConfSenha        { get; set; }
        public bool Administrador      { get; set; }

        [ScriptIgnore]
        [IgnoreDataMember]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Email, "Usuario");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Usuario()
        {

        }

        public Usuario(string myEmail)
        {
            Identity = new GenericIdentity(myEmail, "Usuario");
        }
    }
}
