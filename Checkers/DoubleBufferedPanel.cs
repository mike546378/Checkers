using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    //Small class to get instance of a double buffered panel
    class DoubleBufferedPanel : Panel {public DoubleBufferedPanel(){this.DoubleBuffered = true;}}
}
