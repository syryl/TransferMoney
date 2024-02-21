using dciSphere.Core.Models;
using dciSphere.Interaction.Banks.Dto;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks.Extensions;
public static class BankExtensions
{
    public static BankDto? MapToDto(this Bank? value)
    {
        return value is null ? null : new(Id: value.Id, Name: value.Name);
    }
}
