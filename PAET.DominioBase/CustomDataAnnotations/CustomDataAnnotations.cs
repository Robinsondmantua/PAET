using System;
using System.ComponentModel.DataAnnotations;

namespace PAET.DominioBase.Entidades_Dominio.CustomDataAnnotations
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DNIAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^[0-9XYZxyz]{1}[0-9]{7}[A-Za-z]{1}$";
        public DNIAttribute() : base(pattern)
        {            
            ErrorMessage = "Formato DNI incorrecto.";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DNINIECIFAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^[0-9A-Za-z]{1}[0-9]{7}[0-9A-Za-z]{1}$";

        public DNINIECIFAttribute() : base(pattern)
        {            
            ErrorMessage = "Formato CIF/DNI/NIE incorrecto.";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NumeroAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^\d*$";

        public NumeroAttribute() : base(pattern)
        {
            ErrorMessage = "Valor numérico, sin letras ni signos de puntación";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NumeroDosDecimalesAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^(\d+(?:[\.\,]\d{2})?)$";

        public NumeroDosDecimalesAttribute() : base(pattern)
        {
            ErrorMessage = "Valor numérico con 2 decimales opcionales.";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ContrasenaSeguraAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^(?=^.{8,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        public ContrasenaSeguraAttribute() : base(pattern)
        {
            ErrorMessage = "Mínimo 8 caracteres, entre ellos al menos: una minúscula, una mayúscula, uno especial(!@#$%^&*) y un número.";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TelefonoAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^((\+?[0-9]{3}([ \t|\-])?)?[9|6|7]((\d{1}([ \t|\-])?[0-9]{3})|(\d{2}([ \t|\-])?[0-9]{2}))([ \t|\-])?[0-9]{2}([ \t|\-])?[0-9]{2})$";

        public TelefonoAttribute() : base(pattern)
        {
            ErrorMessage = "Número de teléfono incorrecto.";
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CodigoPostalAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^\d{5}(-\d{4})?$";
        public CodigoPostalAttribute() : base(pattern)
        {
            ErrorMessage = "Código Postal incorrecto.";
        }
    }

}
