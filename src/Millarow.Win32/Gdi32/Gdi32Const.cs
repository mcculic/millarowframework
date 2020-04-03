namespace Millarow.Win32.Gdi32
{
    public static class Gdi32Const
    {
        //Font Weights
        public const int FW_DONTCARE = 0;
        public const int FW_THIN = 100;
        public const int FW_EXTRALIGHT = 200;
        public const int FW_LIGHT = 300;
        public const int FW_NORMAL = 400;
        public const int FW_MEDIUM = 500;
        public const int FW_SEMIBOLD = 600;
        public const int FW_BOLD = 700;
        public const int FW_EXTRABOLD = 800;
        public const int FW_HEAVY = 900;
        public const int FW_ULTRALIGHT = FW_EXTRALIGHT;
        public const int FW_REGULAR = FW_NORMAL;
        public const int FW_DEMIBOLD = FW_SEMIBOLD;
        public const int FW_ULTRABOLD = FW_EXTRABOLD;
        public const int FW_BLACK = FW_HEAVY;

        public const int OEM_CHARSET = 255;

        public const int OUT_DEFAULT_PRECIS = 0;
        public const int OUT_STRING_PRECIS = 1;
        public const int OUT_CHARACTER_PRECIS = 2;
        public const int OUT_STROKE_PRECIS = 3;
        public const int OUT_TT_PRECIS = 4;
        public const int OUT_DEVICE_PRECIS = 5;
        public const int OUT_RASTER_PRECIS = 6;
        public const int OUT_TT_ONLY_PRECIS = 7;
        public const int OUT_OUTLINE_PRECIS = 8;
        public const int OUT_SCREEN_OUTLINE_PRECIS = 9;
        public const int OUT_PS_ONLY_PRECIS = 10;

        public const int CLIP_DEFAULT_PRECIS = 0;
        public const int CLIP_CHARACTER_PRECIS = 1;
        public const int CLIP_STROKE_PRECIS = 2;
        public const int CLIP_MASK = 0xf;
        public const int CLIP_LH_ANGLES = (1 << 4);
        public const int CLIP_TT_ALWAYS = (2 << 4);
        public const int CLIP_DFA_DISABLE = (4 << 4);
        public const int CLIP_EMBEDDED = (8 << 4);

        public const int DEFAULT_QUALITY = 0;
        public const int DRAFT_QUALITY = 1;
        public const int PROOF_QUALITY = 2;

        public const int DEFAULT_PITCH = 0;
        public const int FIXED_PITCH = 1;
        public const int VARIABLE_PITCH = 2;
    }
}