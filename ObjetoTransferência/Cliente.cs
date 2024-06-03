using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Cliente
    {
        
        public int codFilial { get; set; }//código da filial
        public DateTime? dataCadastroCliente { get; set; }//data de cadastro        
        public int idCliente { get; set; }//id do cliente        
        public int codCliente { get; set; }//código do cliente        
        public string nomeCliente { get; set; }//nome do cliente       
        public string fantasiaCliente { get; set; }//fantasia do cliente        
        public string cpfCliente { get; set; }//cpf do cliente        
        public string cnpjCliente { get; set; }//cnpj do cliente
        public int codAtividadeCliente { get; set; }//código da atividade cliente        
        public string descAtividadeCliente { get; set; }//descrição do cliente    
        public string enderecoPadraoCliente { get; set; }//Endereço de entrega padrão        
        public int codRotaCliente { get; set; }//código da rota do cliente        
        public string rotaCliente { get; set; }//rota do cliente        
        public int seqEntregaCliente { get; set; }//sequencia de entrega        
        public string bairroCliente { get; set; }//bairro do cliente        
        public string cidadeCliente { get; set; }//cidade do cliente        
        public string ufCliente { get; set; }//estado do cliente        
        public int codPracaCliente { get; set; }//código da praça do cliente        
        public string descPracaCliente { get; set; }//descrição da praça do cliente        
        public string enderecoCliente { get; set; }//endereco do cliente        
        public string numeroCliente { get; set; }//numero do cliente        
        public string cepCliente { get; set; }//cep do cliente        
        public string foneCliente { get; set; }//fone do cliente        
        public string celularCliente { get; set; }//celular do cliente        
        public string emailCliente { get; set; }//email do cliente
        public string latitudeCliente { get; set; }//latitude do cliente 
        public string longitudeCliente { get; set; }//longitude do cliente 
        public bool agendamentoCliente { get; set; }//cliente exige agendamento        
        public bool auditoriaPedido { get; set; }//cliente auditoria        
        public bool caixaFechadaCliente { get; set; }//cliente exige caixa fechada        
        public bool compartilhado { get; set; }//cliente rota compartilhada        
        public bool validadeCliente { get; set; }//cliente exige validade        
        public int diasValidadeCliente { get; set; }//cliente exige dia de validade        
        public bool naoDividirCarga { get; set; }//cliente não aceita dividir carga        
        public bool paletizadoCliente { get; set; }//cliente exige paletizado        
        public string observacaoCliente { get; set; }//cliente observação        
        public bool statusCliente { get; set; }//cliente status

    }
}
