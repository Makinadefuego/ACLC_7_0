using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace ACLC.Views;

public partial class ModalCarga : Popup
{
    public ModalCarga(string msg)
    {
        InitializeComponent();
        Label.Text = msg;
    }
}