using Assembly.Test;
using AssemblyLoadExample.domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblyLoadExample
{
    public partial class Form1 : Form
    {
        List<AppDomain> domains = new List<AppDomain>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();

            this.ListDomainAssemblies(AppDomain.CurrentDomain);
            foreach (var domain in this.domains)
            {
                this.ListDomainAssemblies(domain);
            }
        }
        private void ListDomainAssemblies(AppDomain domain)
        {
            foreach (var assembly in domain.GetAssemblies())
                this.listBox1.Items.Add(domain.FriendlyName + assembly.FullName);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            MyAssembly.CallMeToLoad();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                for (var i = 0; i < 10000; i++)
                {
                    var isolatedWrapper = new IsolateToNewDomain<MyWork>(this.textBox1.Text + i);
                    isolatedWrapper.Instance.Execute<string>("test");
                    isolatedWrapper.Dispose();

                    //this.domains.Add(isolatedWrapper.Domain);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 10000; i++)
            {
                var domain = this.FindDomain(this.textBox1.Text + i);
                if (domain != null)
                {
                    this.domains.Remove(domain);
                    AppDomain.Unload(domain);

                }
            }
            GC.Collect();
        }

        private AppDomain FindDomain(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach(var domain in this.domains)
                {
                    if (domain.FriendlyName == text)
                        return domain;
                }
            }
            return null;
        }
    }
}
