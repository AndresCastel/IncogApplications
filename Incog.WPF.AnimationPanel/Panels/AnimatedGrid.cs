using Incog.WPF.AnimationPanel.Animators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Incog.WPF.AnimationPanel.Panels
{
    public class AnimatedGrid : Grid
    {
        private DispatcherTimer animationTimer;
        private DateTime lastArrange = DateTime.MinValue;

        public IArrangeAnimator Animator { get; set; }

        public AnimatedGrid()
            : this(new FractionDistanceAnimator(0.25), TimeSpan.FromSeconds(0.025))
        {
        }

        public AnimatedGrid(IArrangeAnimator animator, TimeSpan animationInterval)
        {
            animationTimer = AnimationBase.CreateAnimationTimer(this, animationInterval);
            Animator = animator;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            finalSize = base.ArrangeOverride(finalSize);

            AnimationBase.Arrange(Math.Max(50, (DateTime.Now - lastArrange).TotalSeconds), this, Children, Animator);
            lastArrange = DateTime.Now;

            return finalSize;
        }
    }
}
