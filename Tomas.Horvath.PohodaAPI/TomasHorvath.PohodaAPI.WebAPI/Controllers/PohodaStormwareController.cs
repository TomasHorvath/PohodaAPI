using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Serialization;

namespace TomasHorvath.PohodaAPI.WebAPI.Controllers
{
	public class PohodaStormwareController : ApiController
	{

		[HttpGet]
		[Route("stormware/stock/")]
		public string Stock()
		{

			string appDataPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data");


			string file = Path.Combine(appDataPath, "zasoby_04_v2.0.xml");
			byte[] xmlMessageData = File.ReadAllBytes(file);

			var serializer = new XmlSerializer(typeof(PohodaAPI.Entity.responsePackType));

			PohodaAPI.Entity.responsePackType result;

			using (MemoryStream ms = new MemoryStream(xmlMessageData))
			{
				result = (PohodaAPI.Entity.responsePackType)serializer.Deserialize(ms);

				PohodaAPI.Entity.listStockType stock = result.responsePackItem[0].Item as PohodaAPI.Entity.listStockType;
				return string.Format("celkem produktů : {0}", stock.Items.Count);

			}

			
		}
	}
}
