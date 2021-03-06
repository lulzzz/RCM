﻿using RCM.Domain.Models.ClienteModels;
using RCM.Domain.Validators.ClienteCommandValidators;

namespace RCM.Domain.Commands.ClienteCommands
{
    public class AddClienteCommand : ClienteCommand
    {
        public AddClienteCommand(string nome, ClienteTipoEnum tipo, ClientePontuacaoEnum pontuacao, string descricao)
        {
            Nome = nome;
            Tipo = tipo;
            Pontuacao = pontuacao;
            Descricao = descricao;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddClienteCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
