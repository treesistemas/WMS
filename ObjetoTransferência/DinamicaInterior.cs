using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class DinamicaInterior
    {
        
        public DateTime maniData { get; set; } //mani_data
        public string rotaDescricao { get; set; } //rota_descricao
        public int maniCodigo { get; set; } //mani_codigo
        public int pediCodigo { get; set; } //ped_codigo
        public int Cliente { get; set; } //cli_id
        public double Peso { get; set; } //ped_peso
        public double Valor { get; set; } //ped_total
        public string Apelido { get; set; } //mot_apelido
        public string Placa { get; set; } //vei_placa
        public string Cidade { get; set; } //cid_nome
        public int barCodigo { get; set; } //bar_codigo
        public int cidCodigo { get; set; } //cid_codigo
        public int motCodigo { get; set; } //mot_codigo
        public int rotaCodigo { get; set; } //rota_codigo
        public int veiCodigo { get; set; } //vei_codigo
        public int  tipoCodigo{ get; set; } //tipo_codigo


    }
}
