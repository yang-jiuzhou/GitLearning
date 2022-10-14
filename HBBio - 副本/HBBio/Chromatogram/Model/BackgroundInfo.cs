using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    public enum EnumBackground
    {
        Marker,
        Coll_M,
        Coll_A,
        Valve,
        Phase
    }

    public class BackgroundInfo
    {
        private Color m_marker = Color.White;
        /// <summary>
        /// 标记的颜色
        /// </summary>
        public Color MMarkerColor 
        { 
            get
            {
                return m_marker;
            }
            set
            {
                m_marker = value;
                MMarkerBrush = new SolidBrush(value);
                MMarkerPen = new Pen(value);
            }
        }
        public Brush MMarkerBrush { get; set; }
        public Pen MMarkerPen { get; set; }
        public bool MMarkerVisible { get; set; }
        public bool MMarkerDirection { get; set; }

        private Color m_collM = Color.White;
        /// <summary>
        /// 手动收集的颜色
        /// </summary>
        public Color MCollColorM
        {
            get
            {
                return m_collM;
            }
            set
            {
                m_collM = value;
                MCollBrushM = new SolidBrush(value);
                MCollPenM = new Pen(value);
            }
        }
        public Brush MCollBrushM { get; set; }
        public Pen MCollPenM { get; set; }
        public bool MCollMVisible { get; set; }
        public bool MCollMDirection { get; set; }

        private Color m_collA = Color.White;
        /// <summary>
        /// 自动收集的颜色
        /// </summary>
        public Color MCollColorA
        {
            get
            {
                return m_collA;
            }
            set
            {
                m_collA = value;
                MCollBrushA = new SolidBrush(value);
                MCollPenA = new Pen(value);
            }
        }
        public Brush MCollBrushA { get; set; }
        public Pen MCollPenA { get; set; }
        public bool MCollAVisible { get; set; }
        public bool MCollADirection { get; set; }

        private Color m_valve = Color.White;
        /// <summary>
        /// 切阀的颜色
        /// </summary>
        public Color MValveColor
        {
            get
            {
                return m_valve;
            }
            set
            {
                m_valve = value;
                MValveBrush = new SolidBrush(value);
                MValvePen = new Pen(value);
            }
        }
        public Brush MValveBrush { get; set; }
        public Pen MValvePen { get; set; }
        public bool MValveVisible { get; set; }
        public bool MValveDirection { get; set; }

        private Color m_phase = Color.White;
        /// <summary>
        /// 阶段名称的颜色
        /// </summary>
        public Color MPhaseColor
        {
            get
            {
                return m_phase;
            }
            set
            {
                m_phase = value;
                MPhaseBrush = new SolidBrush(value);
                MPhaseBrushOpacity = new SolidBrush(Color.FromArgb(50, value));
                MPhasePen = new Pen(value);
            }
        }
        public Brush MPhaseBrush { get; set; }
        public Brush MPhaseBrushOpacity { get; set; }
        public Pen MPhasePen { get; set; }
        public bool MPhaseVisible { get; set; }
        public bool MPhaseDirection { get; set; }


        public BackgroundInfo()
        {
            MMarkerColor = Color.Red;
            MCollColorM = Color.Blue;
            MCollColorA = Color.Blue;
            MValveColor = Color.Black;
            MPhaseColor = Color.Pink;

            MMarkerVisible = false;
            MCollMVisible = false;
            MCollAVisible = false;
            MValveVisible = false;
            MPhaseVisible = false;

            MMarkerDirection = true;
            MCollMDirection = true;
            MCollADirection = true;
            MValveDirection = true;
            MPhaseDirection = true;
        }
    }
}
