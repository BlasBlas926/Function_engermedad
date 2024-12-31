using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECE.Entities
{
    [Table("tc_enfermedad_cronica")]
    public class EnfermedadCronica
    {
        public int id_enf_cronica { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        [NotMapped]
        public DateTime fecha_registro2 { get; set; }
        [JsonIgnore]
        public DateOnly fecha_registro
        {
            get => DateOnly.FromDateTime(fecha_registro2);
            set => fecha_registro2 = value.ToDateTime(TimeOnly.MaxValue);
        }
        public DateTime fecha_inicio2 { get; set; }
        [JsonIgnore]
        public DateOnly fecha_inicio
        {
            get => DateOnly.FromDateTime(fecha_inicio2);
            set => fecha_inicio2 = value.ToDateTime(TimeOnly.MaxValue);
        }
        public bool estado { get; set; }

        public DateTime fecha_actualizacion2 { get; set; }

        [JsonIgnore]
        public DateOnly fecha_actualizacion
        {
            get => DateOnly.FromDateTime(fecha_actualizacion2);
            set => fecha_actualizacion2 = value.ToDateTime(TimeOnly.MaxValue);
        }
    }

}