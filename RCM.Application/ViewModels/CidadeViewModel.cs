﻿using System.ComponentModel.DataAnnotations;

namespace RCM.Application.ViewModels
{
    public class CidadeViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo Id é requerido.")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é requerido.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O campo nome deve ter entre 4 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O estado relacionado é requerido.")]
        public int EstadoId { get; set; }
        public EstadoViewModel Estado { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}