using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;

namespace FerreteriaGHome.Web.Data.Entities
{
    public class Sprint: IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre del Sprint")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Objetivo del Sprint")]
        public string Objective { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Sprint), "StartDateValidation")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Sprint), "EndDateValidation")]
        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDate { get; set; }

        public ICollection<Activity> Activities { get; set; }
        public ICollection<ProyectSprint> ProyectSprints { get; set; }
        public ICollection<SprintActivity> SprintActivities { get; set; }

        public static ValidationResult StartDateValidation(DateTime startDate, ValidationContext context)
        {
            if(startDate < DateTime.Now)
            {
                return new ValidationResult("La fecha de inicio no puede ser menor a la actual.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult EndDateValidation(DateTime endDate, ValidationContext context)
        {
            var sprint = context.ObjectInstance as Sprint;
            if(endDate <= sprint.StartDate)
            {
                return new ValidationResult("La fecha de finalización debe ser posterior a la fecha de inicio.");
            }
            return ValidationResult.Success;
        }
    }
}
