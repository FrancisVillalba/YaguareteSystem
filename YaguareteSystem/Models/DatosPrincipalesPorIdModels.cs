using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class DatosPrincipalesPorIdModels
    {

        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("odata.id")]
        public Guid OdataId { get; set; }

        [JsonProperty("odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("odata.editLink")]
        public string OdataEditLink { get; set; }

        [JsonProperty("AttachmentFiles@odata.navigationLinkUrl")]
        public string AttachmentFilesOdataNavigationLinkUrl { get; set; }

        [JsonProperty("AttachmentFiles")]
        public AttachmentFile[] AttachmentFiles { get; set; }

        [JsonProperty("FileSystemObjectType")]
        public long FileSystemObjectType { get; set; }

        [JsonProperty("Id")]
        public long DatosPrincipalesPorIdModelsId { get; set; }

        [JsonProperty("ServerRedirectedEmbedUri")]
        public object ServerRedirectedEmbedUri { get; set; }

        [JsonProperty("ServerRedirectedEmbedUrl")]
        public string ServerRedirectedEmbedUrl { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("docUltimaAccion")]
        public string DocUltimaAccion { get; set; }

        [JsonProperty("docUltimoComentario")]
        public string DocUltimoComentario { get; set; }

        [JsonProperty("docDepartamento")]
        public string DocDepartamento { get; set; }

        [JsonProperty("comTipoLegajo")]
        public string ComTipoLegajo { get; set; }

        [JsonProperty("comProveedorRuc")]
        public string ComProveedorRuc { get; set; }

        [JsonProperty("comProveedorNombre")]
        public string ComProveedorNombre { get; set; }

        [JsonProperty("comEmpresa")]
        public string ComEmpresa { get; set; }

        [JsonProperty("comNroSP")]
        public string ComNroSp { get; set; }

        [JsonProperty("comComentarioCompras")]
        public string ComComentarioCompras { get; set; }

        [JsonProperty("comAprobado")]
        public string ComAprobado { get; set; }

        [JsonProperty("comFechaRecibido")]
        public object ComFechaRecibido { get; set; }

        [JsonProperty("comFechaProcesado")]
        public object ComFechaProcesado { get; set; }

        [JsonProperty("recepNroFactura")]
        public string RecepNroFactura { get; set; }

        [JsonProperty("recepTipoDocumento")]
        public string RecepTipoDocumento { get; set; }

        [JsonProperty("recepFechaFactura")]
        public object RecepFechaFactura { get; set; }

        [JsonProperty("recepTimbrado")]
        public object RecepTimbrado { get; set; }

        [JsonProperty("recepMontoFactura")]
        public object RecepMontoFactura { get; set; }

        [JsonProperty("recepOriginalCopia")]
        public string RecepOriginalCopia { get; set; }

        [JsonProperty("recepComentario")]
        public string RecepComentario { get; set; }

        [JsonProperty("recepAccion")]
        public string RecepAccion { get; set; }

        //[JsonProperty("recepRecibido")]
        //public DateTimeOffset RecepRecibido { get; set; }

        [JsonProperty("recepProcesado")]
        public object RecepProcesado { get; set; }

        [JsonProperty("impAccion")]
        public object ImpAccion { get; set; }

        [JsonProperty("imprComentario")]
        public object ImprComentario { get; set; }

        [JsonProperty("impRecibido")]
        public object ImpRecibido { get; set; }

        [JsonProperty("impProcesado")]
        public object ImpProcesado { get; set; }

        [JsonProperty("cuentaPagarAccion")]
        public object CuentaPagarAccion { get; set; }

        [JsonProperty("cuentaPagarComentario")]
        public object CuentaPagarComentario { get; set; }

        [JsonProperty("cuentaPagarRecibido")]
        public object CuentaPagarRecibido { get; set; }

        [JsonProperty("cuentaPagarProcesado")]
        public object CuentaPagarProcesado { get; set; }

        [JsonProperty("revComprasPagarAccion")]
        public object RevComprasPagarAccion { get; set; }

        [JsonProperty("revComprasRechazada")]
        public object RevComprasRechazada { get; set; }

        [JsonProperty("revComprasComentario")]
        public object RevComprasComentario { get; set; }

        [JsonProperty("revComprasRecibido")]
        public object RevComprasRecibido { get; set; }

        [JsonProperty("revComprasProcesado")]
        public object RevComprasProcesado { get; set; }

        [JsonProperty("solAccion")]
        public object SolAccion { get; set; }

        [JsonProperty("solComentario")]
        public object SolComentario { get; set; }

        [JsonProperty("solRecibido")]
        public object SolRecibido { get; set; }

        [JsonProperty("solProcesado")]
        public object SolProcesado { get; set; }

        [JsonProperty("tesoAccion")]
        public object TesoAccion { get; set; }

        [JsonProperty("tesoComentario")]
        public object TesoComentario { get; set; }

        [JsonProperty("tesoRecibido")]
        public object TesoRecibido { get; set; }

        [JsonProperty("tesoProcesado")]
        public object TesoProcesado { get; set; }

        [JsonProperty("ContentTypeId")]
        public string ContentTypeId { get; set; }

        [JsonProperty("ComplianceAssetId")]
        public object ComplianceAssetId { get; set; }

        [JsonProperty("FlujoDoc")]
        public object FlujoDoc { get; set; }

        [JsonProperty("comSolicitante")]
        public string ComSolicitante { get; set; }

        [JsonProperty("comSolicitanteNombre")]
        public string ComSolicitanteNombre { get; set; }

        [JsonProperty("comCreadoPor")]
        public string ComCreadoPor { get; set; }

        [JsonProperty("recepProcesadoPor")]
        public object RecepProcesadoPor { get; set; }

        [JsonProperty("recepProcesadoNombre")]
        public object RecepProcesadoNombre { get; set; }

        [JsonProperty("recepAprobadoPor")]
        public object RecepAprobadoPor { get; set; }

        [JsonProperty("recepAprobadoNombre")]
        public object RecepAprobadoNombre { get; set; }

        [JsonProperty("impProcesadoPor")]
        public object ImpProcesadoPor { get; set; }

        [JsonProperty("impProcesadoNombre")]
        public object ImpProcesadoNombre { get; set; }

        [JsonProperty("cuentaPagarProcesadoPor")]
        public object CuentaPagarProcesadoPor { get; set; }

        [JsonProperty("cuentaPagarProcesadoNombre")]
        public object CuentaPagarProcesadoNombre { get; set; }

        [JsonProperty("revComprasProcesadoPor")]
        public object RevComprasProcesadoPor { get; set; }

        [JsonProperty("revComprasProcesadoNombre")]
        public object RevComprasProcesadoNombre { get; set; }

        [JsonProperty("solProcesadoPor")]
        public object SolProcesadoPor { get; set; }

        [JsonProperty("solProcesadoNombre")]
        public object SolProcesadoNombre { get; set; }

        [JsonProperty("tesoProcesadoPor")]
        public object TesoProcesadoPor { get; set; }

        [JsonProperty("tesoProcesadoNombre")]
        public object TesoProcesadoNombre { get; set; }

        [JsonProperty("comCreadoNombre")]
        public string ComCreadoNombre { get; set; }

        [JsonProperty("comProveedorCodSap")]
        public object ComProveedorCodSap { get; set; }

        [JsonProperty("comTipoMoneda")]
        public string ComTipoMoneda { get; set; }

        [JsonProperty("comMontoTotal")]
        public object ComMontoTotal { get; set; }

        [JsonProperty("recepFechaProcesado")]
        public object RecepFechaProcesado { get; set; }

        [JsonProperty("docUltimoUsuarioAccion")]
        public string DocUltimoUsuarioAccion { get; set; }

        [JsonProperty("docFechaUltimaAccion")]
        public object DocFechaUltimaAccion { get; set; }

        [JsonProperty("docIdUltimaAccion")]
        public object DocIdUltimaAccion { get; set; }

        [JsonProperty("docDepartamentoAnterior")]
        public object DocDepartamentoAnterior { get; set; }

        [JsonProperty("docIdMovimiento")]
        public object DocIdMovimiento { get; set; }

        [JsonProperty("cuentaPagarNroAsiento")]
        public object CuentaPagarNroAsiento { get; set; }

        [JsonProperty("recepNroDespacho")]
        public object RecepNroDespacho { get; set; }

        [JsonProperty("recepTipoFactura")]
        public object RecepTipoFactura { get; set; }

        [JsonProperty("recepTipoMoneda")]
        public object RecepTipoMoneda { get; set; }

        [JsonProperty("recepProveedorId")]
        public object RecepProveedorId { get; set; }

        [JsonProperty("recepFacturaAsociadaNotaCredito")]
        public object RecepFacturaAsociadaNotaCredito { get; set; }
        [JsonProperty("recepFechaAddFactura")]
        public object RecepFechaAddFactura { get; set; }

        [JsonProperty("ID")]
        public object Id { get; set; }

        [JsonProperty("Modified")]
        public DateTimeOffset Modified { get; set; }

        [JsonProperty("Created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("AuthorId")]
        public object AuthorId { get; set; }

        [JsonProperty("EditorId")]
        public object EditorId { get; set; }

        [JsonProperty("OData__UIVersionString")]
        public string ODataUiVersionString { get; set; }

        [JsonProperty("Attachments")]
        public bool Attachments { get; set; }

        [JsonProperty("GUID")]
        public Guid Guid { get; set; }
    }
}
