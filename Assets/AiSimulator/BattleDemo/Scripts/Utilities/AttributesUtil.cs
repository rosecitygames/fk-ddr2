using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Traits;

namespace IndieDevTools.Demo.BattleSimulator
{
    public static class AttributesUtil
    {
        // Move Speed
        const float minSpeed = 0.0f;
        const float maxSpeed = 10.0f;
        const float defaultSpeed = 1.0f;

        const float minMoveSpeed = 0.0f;
        const float maxMoveSpeed = 0.05f;

        const string speedAttributeId = "Speed";

        public static float GetMoveSpeed(IStatsCollection statsCollection)
        {
            return Mathf.Lerp(minMoveSpeed, maxMoveSpeed, GetSpeedPercentage(statsCollection));
        }

        static float GetSpeedPercentage(IStatsCollection statsCollection)
        {
            return Mathf.InverseLerp(minSpeed, maxSpeed, GetSpeed(statsCollection));
        }

        static float GetSpeed(IStatsCollection statsCollection)
        {
            ITrait attribute = statsCollection.GetStat(speedAttributeId);
            if (attribute == null)
            {
                return defaultSpeed;
            }
            return Mathf.Clamp(attribute.Quantity * 1.0f, minSpeed, maxSpeed);
        }

        // Move Radius
        const string moveRadiusAttributeId = "MoveRadius";
        public static int GetMoveRadius(IStatsCollection statsCollection)
        {
            ITrait attribute = statsCollection.GetStat(moveRadiusAttributeId);
            if (attribute == null)
            {
                return 0;
            }
            return attribute.Quantity;
        }

        // Attack Strength
        const int minAttackStrength = 0;
        const int maxAttackStrength = 10;
        const int defaultAttackStrength = 0;

        const string attackStrengthAttributeId = "Attack";
        public static int GetAttackStrength(IStatsCollection statsCollection)
        {
            ITrait attribute = statsCollection.GetStat(attackStrengthAttributeId);
            if (attribute == null)
            {
                return defaultAttackStrength;
            }
            return Mathf.Clamp(attribute.Quantity, minAttackStrength, maxAttackStrength);
        }

        public static int GetRandomAttackStrength(IStatsCollection statsCollection)
        {
            float attackStrength = GetAttackStrength(statsCollection);
            return Mathf.RoundToInt(Random.Range(minAttackStrength, attackStrength));
        }

        // Defense Strength
        const int minDefenseStrength = 0;
        const int maxDefenseStrength = 10;
        const int defaultDefenseStrength = 0;

        const string defenseStrengthAttributeId = "Defense";
        public static int GetDefenseStrength(IStatsCollection statsCollection)
        {
            ITrait attribute = statsCollection.GetStat(defenseStrengthAttributeId);
            if (attribute == null)
            {
                return defaultDefenseStrength;
            }
            return Mathf.Clamp(attribute.Quantity, minDefenseStrength, maxDefenseStrength);
        }

        public static int GetRandomDefenseStrength(IStatsCollection statsCollection)
        {
            float defenseStrength = GetDefenseStrength(statsCollection);
            return Mathf.RoundToInt(Random.Range(minDefenseStrength, defenseStrength));
        }

        // Health
        const string healthAttributeId = "Health";
        public static int GetHealth(IStatsCollection statsCollection)
        {
            ITrait attribute = statsCollection.GetStat(healthAttributeId);
            if (attribute == null)
            {
                return 0;
            }
            return attribute.Quantity;
        }

        public static void SetHealth(IStatsCollection statsCollection, int quantity)
        {
            ITrait attribute = statsCollection.GetStat(healthAttributeId);
            if (attribute != null)
            {
                attribute.Quantity = quantity;
            }
        }
    }
}
