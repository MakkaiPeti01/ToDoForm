﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ToDoForm
{   
    public partial class frmKezdo : Form
    {
        private List<string> todos = new List<string>();
        public frmKezdo()
        {
            InitializeComponent();
            StreamReader be = new StreamReader("todoItems.txt");
            while (!be.EndOfStream)
            {
                todos.Add(be.ReadLine());
            }
            be.Close();
        }

        private void mKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmKezdo_Shown(object sender, EventArgs e)
        {
            tbBevitel.Text = "";
            lbTodo.Items.Clear();
            tbBevitel.Focus();
        }

        private void btnListabol_Click(object sender, EventArgs e)
        {
            frmLista formLista = new frmLista(todos);
            var result = formLista.ShowDialog();
            if (result==DialogResult.OK)
            {
                tbBevitel.Text = formLista.SelectedTodo;
            }
            tbBevitel.Focus();
            tbBevitel.SelectionStart = tbBevitel.Text.Length;
            tbBevitel.SelectionLength = 0;
        }

        private void btnFelvitel_Click(object sender, EventArgs e)
        {
            string todo = tbBevitel.Text.Trim();
            if (todo != "" && !lbTodo.Items.Contains(todo))
            {
                lbTodo.Items.Add(todo);
                tbBevitel.Text = "";
            }
        }

        private void btnEltavolit_Click(object sender, EventArgs e)
        {
            lbTodo.Items.Clear();
        }

        private void btnKivesz_Click(object sender, EventArgs e)
        {
            int index = lbTodo.SelectedIndex;
            if (index > -1)
            {
                lbTodo.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Válassz ki egy elemet!","Nincs kiválasztva",MessageBoxButtons.OK
                    ,MessageBoxIcon.Warning);
            }
        }

        private void mBeolvasas_Click(object sender, EventArgs e)
        {
            if (ofdMegnyitas.ShowDialog() == DialogResult.OK)
            {
                lbTodo.Items.Clear();
                StreamReader olvas = new StreamReader(ofdMegnyitas.FileName);
                while (!olvas.EndOfStream)
                {
                    lbTodo.Items.Add(olvas.ReadLine());
                }
                olvas.Close();
            }
        }

        private void mMentes_Click(object sender, EventArgs e)
        {
            if (lbTodo.Items.Count>0)
            {
            svdMentes.FileName = FileNameDate();
            if (svdMentes.ShowDialog() == DialogResult.OK)
            {
                StreamWriter iro = new StreamWriter(svdMentes.FileName);
                foreach (var i in lbTodo.Items)
                {
                    iro.WriteLine(i);
                }
                iro.Close();
                MessageBox.Show("Sikeres mentés","Mentve", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            else
            {
                MessageBox.Show("Nincs mit menteni", "Nem lett mentve", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string FileNameDate()
        {
            var datum = DateTime.Now;
            string ev = datum.Year.ToString();
            string honap = "";
            if (datum.Month < 10)
            {
                honap = "0" + datum.Month.ToString();
            }
            else
            {
                honap = datum.Month.ToString();
            }
            string nap = "";
            if (datum.Day < 10)
            {
                nap = "0" + datum.Day.ToString();        
            }
            else
            {
                nap = datum.Day.ToString();
            }
            return ev + honap + nap;
        }
    }
}
