using System;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using StackExchange.Redis;
using System.Collections.Generic;

namespace PSStackExchange.Redis
{
    [Cmdlet(VerbsCommunications.Connect,"Multiplexer")]
    [OutputType(typeof(FavoriteStuff))]
    public class ConnectMultiplexerCmdletCommand : PSCmdlet
    {
        /* TODO provide connectionstring as input */
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string Hostname { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public bool UseSsl { get; set; } = true;

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public bool AllowAdmin { get; set; } = false;

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public bool AbortConnect { get; set; } = false;

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public SwitchParameter Passthru { get; set; }

        private string _configuration;

        private ConnectionMultiplexer _multiplexer;

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Hostname);
            stringBuilder.Append(",");
            stringBuilder.Append("ssl=");
            stringBuilder.Append(UseSsl);
            stringBuilder.Append(",");
            stringBuilder.Append("password=");
            stringBuilder.Append(Password);
            stringBuilder.Append(",");
            stringBuilder.Append("allowAdmin=");
            stringBuilder.Append(AllowAdmin);
            stringBuilder.Append(",");
            stringBuilder.Append("abortConnect=");            
            stringBuilder.Append(AbortConnect);
            if(!String.IsNullOrEmpty(Name))
            {
                stringBuilder.Append(",");
                stringBuilder.Append("name=");
                stringBuilder.Append(Name);
            }

            _configuration = stringBuilder.ToString();            
            WriteVerbose("connection string: " + _configuration.Replace(Password, "***********"));
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            try
            {
                _multiplexer = MyMultiplexer.Connect(_configuration);
            }
            catch (System.Exception e)
            {
                WriteVerbose("Failed to connect, error was " + e.ToString() + " error type " + e.GetType());
            }
            /*
            get-datebase
            add values
            get values
             */
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            if(Passthru.IsPresent)
            {
                WriteObject(_multiplexer);
            }
        }
    }
}