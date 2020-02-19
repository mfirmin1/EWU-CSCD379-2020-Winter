using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GroupInputMetadata))]
    public partial class GroupInput 
    {
    }
    public class GroupInputMetadata
    {
    }
}
