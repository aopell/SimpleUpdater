using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Form2(args)).ShowDialog();
        }
    }
}
