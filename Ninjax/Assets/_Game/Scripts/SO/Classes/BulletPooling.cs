using Gameplay;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(BulletPooling), menuName = ("Scriptables/" + nameof(BulletPooling)))]
    public class BulletPooling : PoolingBase<BulletBase>
    {
        
    }
}