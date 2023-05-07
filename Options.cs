using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POCAutomacao
{
    public class Options
    {
        [Option('g', "groupId", Required = true, HelpText = "ID do grupo")]
        public string GroupId { get; set; } = null!;

        [Option('r', "reportId", Required = true, HelpText = "ID do relatório")]
        public string ReportId { get; set; } = null!;

        [Option('e', "email", Required = true, HelpText = "E-mail")]
        public string Email { get; set; } = null!;

        [Option('p', "password", Required = true, HelpText = "Senha")]
        public string Password { get; set; } = null!;
    }
}
