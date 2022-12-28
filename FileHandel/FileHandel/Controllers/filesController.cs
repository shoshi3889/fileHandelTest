using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using File = FileHandel.Model.File;
using  FileHandel.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileHandel.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class filesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<File>> Get()

        {
            try
            {
                List<File> listFromDB = DemoDBAction.ReadList();
                return Ok(listFromDB);
            }
            catch
            {
                //write to log
                return BadRequest(Resource.GeneralError);
            }
        }


        
        [HttpPost]
        public ActionResult<List<File>> SaveFile([FromBody] File file)
        {
            try
            {
                List<File> listFromDB = DemoDBAction.ReadList();
                File fileToUpdate = listFromDB.Find(item => item.fileID == file.fileID);
                if (file.fileID == 0)
                {
                    DemoDBAction.InsetFile(file);
                }
                else DemoDBAction.UpdateFile(file);
                return Ok();
            }
            catch
            {
                //write to log
                return BadRequest(Resource.GeneralError);
            }

        }

        
        [HttpPost]
        public ActionResult<List<File>> SplitFile([FromBody] int id)
        {
            try
            {
                File file = DemoDBAction.GetFile(id);
                if (file != null)
                {
                    List<File> files= file.SplitFile(file);
                    return Ok(files);
                }
                throw new Exception(Resource.NullReferene);
            }
            catch
            {
                //write to log
                return BadRequest(Resource.GeneralError);
            }
        }
    }
}