using GameAssistant.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows.Media;

namespace GameAssistant.Services.Animations
{
    /// <summary>
    /// Animation controller base.
    /// </summary>
    public class AnimationControllerBase
    {
        /// <summary>
        /// Animation timer.
        /// </summary>
        protected Timer animationTimer;

        // todo dodać szybkość animacji INTERVAL
        private double _animationInterval;

        public double AnimationInterval
        {
            get => _animationInterval;
            set
            {
                _animationInterval = value;
                animationTimer.Interval = value;
            }
        }


        /// <summary>
        /// Brush to animate.
        /// </summary>
        protected VariableContainer<Brush> brush = new VariableContainer<Brush>(new SolidColorBrush(Colors.Red));

        /// <summary>
        /// List of members (brushContainer containers).
        /// </summary>
        private readonly List<VariableContainer<Brush>> _members = new List<VariableContainer<Brush>>();

        /// <summary>
        /// Adds members.
        /// </summary>
        /// <param name="brushContainer">New member.</param>
        public void AddMember(ref VariableContainer<Brush> brushContainer)
        {
            _members.Add(brushContainer);
#if DEBUG
            Debug.WriteLine("Members: " + _members.Count);
#endif
            if (!animationTimer.Enabled)
                StartAnimate();
        }

        /// <summary>
        /// Removes members.
        /// </summary>
        /// <param name="brushContainer">A member.</param>
        public void RemoveMember(ref VariableContainer<Brush> brushContainer)
        {
            if (_members.Contains(brushContainer))
                _members.Remove(brushContainer);
#if DEBUG
            Debug.WriteLine("Members: " + _members.Count);
#endif
            if (_members.Count == 0)
                StopAnimate();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="brush">Brush to animate.</param>
        /// <param name="animation">Type of animation.</param>
        /// <param name="animationInterval">Refresh time.</param>
        public AnimationControllerBase(double animationInterval = 5)
        {
            _animationInterval = animationInterval;
            animationTimer = new Timer(animationInterval);
            animationTimer.Elapsed += AnimationTimer_Elapsed;
        }

        /// <summary>
        /// Animation method (default is void).
        /// </summary>
        protected virtual void Animate() { }

        /// <summary>
        /// On timer elapsed event.
        /// </summary>
        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Animate();
            foreach (var member in _members)
                member.Variable = brush.Variable;
        }

        /// <summary>
        /// Turn on timer elapsed event.
        /// </summary>
        public void StartAnimate()
        {
#if DEBUG
            if (animationTimer != null)
                Debug.WriteLine("Start Timer Animation");
#endif
            animationTimer?.Start();
        }

        /// <summary>
        /// Turn off timer elapsed event.
        /// </summary>
        public void StopAnimate()
        {
#if DEBUG
            if (animationTimer != null)
                Debug.WriteLine("Stop Timer Animation");
#endif
            animationTimer?.Stop();
        }

    }
}
