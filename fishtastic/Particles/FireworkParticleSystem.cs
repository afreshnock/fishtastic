using fishtastic.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishtastic.Particles
{
    public class FireworkParticleSystem : ParticleSystem
    {

        Color[] colors = new Color[]
        {
            Color.Red, Color.Green, Color.Blue, Color.Orange, Color.Orchid, Color.Aqua
        };

        Color color;
        public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "particle";
            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 100);

            var lifetime = RandomHelper.NextFloat(.5f, 1f);

            var acceleration = -velocity / lifetime;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velocity,acceleration, color ,lifetime: lifetime,rotation: rotation,angularVelocity: angularVelocity, scale : scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);

            particle.Color = Color.White * alpha;

            particle.Scale = .1f + .25f * normalizedLifetime;
        }


        public void PlaceFireWork(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}
