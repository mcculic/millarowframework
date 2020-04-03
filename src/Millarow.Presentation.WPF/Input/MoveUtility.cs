using Millarow.Presentation.WPF.Framework;
using System.Windows;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Input
{
    internal static class MoveUtility
    {
        public static RectManipulations GetRectManipulations(Rect rect, Point pt, Thickness resizeBorderPadding, bool canMove, bool canResize)
        {
            if (!canResize && !canMove)
                return RectManipulations.None;

            var pad = resizeBorderPadding;

            if (rect.Contains(pt))
            {
                if (canMove && rect.Pad(resizeBorderPadding).Contains(pt))
                    return RectManipulations.Move;
                else if (canResize)
                {
                    var ret = RectManipulations.None;

                    if (pt.X - rect.X <= resizeBorderPadding.Left)
                        ret |= RectManipulations.ResizeL;
                    else if (rect.Right - pt.X <= resizeBorderPadding.Right)
                        ret |= RectManipulations.ResizeR;

                    if (pt.Y - rect.Y <= resizeBorderPadding.Top)
                        ret |= RectManipulations.ResizeT;
                    else if (rect.Bottom - pt.Y <= resizeBorderPadding.Bottom)
                        ret |= RectManipulations.ResizeB;

                    return ret;
                }
            }

            return RectManipulations.None;
        }

        public static void GetManipulationsVectors(RectManipulations manipulations, Point mousePos, Point prevMousePos, out Vector positionVector, out Vector sizeVector)
        {
            var deltaX = mousePos.X - prevMousePos.X;
            var deltaY = mousePos.Y - prevMousePos.Y;

            positionVector = new Vector(0, 0);
            sizeVector = new Vector(0, 0);

            if (manipulations.HasFlag(RectManipulations.Move))
            {
                positionVector.X = deltaX;
                positionVector.Y = deltaY;
            }

            if (manipulations.HasFlag(RectManipulations.ResizeR))
                sizeVector.X += deltaX;

            if (manipulations.HasFlag(RectManipulations.ResizeB))
                sizeVector.Y += deltaY;

            if (manipulations.HasFlag(RectManipulations.ResizeL))
            {
                positionVector.X += deltaX;
                sizeVector.X += -deltaX;
            }

            if (manipulations.HasFlag(RectManipulations.ResizeT))
            {
                positionVector.Y += deltaY;
                sizeVector.Y += -deltaY;
            }
        }

        public static Cursor GetCursor(RectManipulations manipulations)
        {
            if (manipulations == RectManipulations.Move)
                return Cursors.SizeAll;
            if (manipulations == RectManipulations.ResizeL || manipulations == RectManipulations.ResizeR)
                return Cursors.SizeWE;
            if (manipulations == RectManipulations.ResizeT || manipulations == RectManipulations.ResizeB)
                return Cursors.SizeNS;
            if (manipulations == (RectManipulations.ResizeL | RectManipulations.ResizeT) || manipulations == (RectManipulations.ResizeR | RectManipulations.ResizeB))
                return Cursors.SizeNWSE;
            if (manipulations == (RectManipulations.ResizeL | RectManipulations.ResizeB) || manipulations == (RectManipulations.ResizeR | RectManipulations.ResizeT))
                return Cursors.SizeNESW;

            return Cursors.Arrow;
        }
    }
}
