using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forca
{
    public partial class Form1 : Form
    {
        public bool iniciar;
        public PictureBox[] pcb;
        public string palavra;
        public byte erro = 1;



        public Form1()
        {
            InitializeComponent();
            
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void btniniciar_Click(object sender, EventArgs e)
        {
            Jogar jogar = new Jogar();
            pictureBox1.Enabled = true;
            pcb = new PictureBox[txtpalavra.Text.Length]; //recebe o tamanho da
            //palavra oculta
            palavra = txtpalavra.Text.ToLower(); //transforma para maiuscula
                                               //cria tracinhos (método pintar) do tamanho da palavra oculta
            for (int cont = 0; cont < txtpalavra.Text.Length; cont++)
            {
                pcb[cont] = new PictureBox();
                pcb[cont].Width = 50;
                pcb[cont].Height = 50;
                pcb[cont].Paint += new PaintEventHandler(jogar.pintar);
                flowLayoutPanel1.Controls.Add(pcb[cont]); //aqui recebe os
                //tracinhos
            }
            btniniciar.Enabled = false;
            txtpalavra.Enabled = false;
            iniciar = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys chave)
        {
            if (!iniciar) return false;
            if (txtpalavra.Text.ToLower().Contains(chave.ToString().ToLower()))
            {
                acertou(chave.ToString());
            }
            else
            {
                errou();
            }
            return base.ProcessCmdKey(ref msg, chave);
        }

        private void reiniciar()
        {
            pictureBox1.Image = Forca.Properties.Resources.img1;
            btniniciar.Enabled = true;
            txtpalavra.Enabled = true;
            iniciar = false;
            txtpalavra.Text = "";
            erro = 0;
            flowLayoutPanel1.Controls.Clear();
        }
        private void errou()
        {
            switch (erro)
            {
                case 1:
                    pictureBox1.Image = Forca.Properties.Resources.img2;
                    break;
                case 2:
                    pictureBox1.Image = Forca.Properties.Resources.img3;

                    break;
                case 3:
                    pictureBox1.Image = Forca.Properties.Resources.img4;

                    break;

                case 4:
                    pictureBox1.Image = Forca.Properties.Resources.img5;

                    break;
                case 5:
                    pictureBox1.Image = Forca.Properties.Resources.img6;
                    break;
                case 6:
                    pictureBox1.Image = Forca.Properties.Resources.img7;

                    MessageBox.Show("PERDEU!! a palavra era: " + txtpalavra.Text);

                    reiniciar();
                    break;

            }
            erro++; //caminha o switch para completar a forca
        }

        private void acertou(string letra)
        {
            for (int cont = 0; cont < txtpalavra.Text.Length; cont++)
            {
                if (txtpalavra.Text[cont].ToString().ToLower() == letra.ToLower())
                {
                    Graphics graf = pcb[cont].CreateGraphics();
                    graf.DrawString(letra, new Font("Times", 25), new
                    SolidBrush(Color.CornflowerBlue), 0, 0);
                    palavra = palavra.Replace(letra.ToLower(), " ");
                }
            }
            if (palavra.Trim() == "") //trim remove espaço em branco
            {
                MessageBox.Show("Parabens sobre o coitado sem ter o quefazer...\n\n" + txtpalavra.Text);
                reiniciar();
            }
        }

    }
}
