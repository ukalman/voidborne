using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Stats;
using Tiles;
using UnityEngine;
using Utilities;

namespace Units
{
    public class BaseUnit : MonoBehaviour
    {
        public string UnitName;
        public Tile OccupiedTile;
        public Faction Faction;
        
        
        public int  MaxHealth; // Total vitality
        public int CurrentHealth;
        
        public Stat Strength; //  Directly impacts their melee damage output.
        public Stat Armor; //  Essential for withstanding attacks in melee combat.
        public Stat Power; //  Increases spell damage and critical spell effects.
        public Stat Intelligence;
        public Stat Dexterity; //  Improves precision, critical strikes, and lockpicking abilities.
        public Stat Agility; // Increases dodging capabilities, essential for a character who might wear lighter armor.
        public Stat Charisma; // Useful for manipulating NPCs or bartering with traders using magical artifacts.
        public Stat Focus; // Enhances critical rate and aiming proficiency, crucial for a ranged fighter.
        
        /*
        public int Health { get; set; } // Total vitality 
        public int Armor { get; set; } //  Essential for withstanding attacks in melee combat.
        public int Strength { get; set; } //  Directly impacts their melee damage output.
        public int Power { get; set; } //  Increases spell damage and critical spell effects.
        public int Intelligence { get; set; } 
        public int Dexterity { get; set; } //  Improves precision, critical strikes, and lockpicking abilities.
        public int Agility { get; set; } // Increases dodging capabilities, essential for a character who might wear lighter armor.
        public int Charisma { get; set; } // Useful for manipulating NPCs or bartering with traders using magical artifacts.
        public int Focus { get; set; } // Enhances critical rate and aiming proficiency, crucial for a ranged fighter.
        */
        public bool IsMoving { get; private set; }
        
        private Coroutine _currentMovementCoroutine;

        private static readonly int Moving = Animator.StringToHash("IsMoving");
        
        [SerializeField]
        protected Animator animator;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void Start()
        {
            IsMoving = false;
            CurrentHealth = MaxHealth;
            //animator = GetComponent<Animator>();
        }

        /*
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TakeDamage(10);
            }
        }
        */

        public virtual void SetAttributes(int strength, int armor, int power, int intelligence, int dexterity, int agility,
            int charisma, int focus)
        {
            Strength.SetValue(strength);
            Armor.SetValue(armor);
            Power.SetValue(power);
            Intelligence.SetValue(intelligence);
            Dexterity.SetValue(dexterity);
            Agility.SetValue(agility);
            Charisma.SetValue(charisma);
            Focus.SetValue(focus);
        }

        public void SetHealth()
        {
            CurrentHealth = MaxHealth;
        }
        
        
        // the parameter path, should be a list containing Tiles, not Vector2's.
        public IEnumerator FollowPath(List<Tile> path)
        {
            IsMoving = true;
            animator.SetBool(Moving, IsMoving);
            Debug.Log(path.Count);

            foreach (Tile tile in path)
            {
                if (tile != OccupiedTile) // Ensure the tile is not the one we're already on
                {
                    _currentMovementCoroutine = StartCoroutine(MoveToTile(tile, 0.5f - (0.05f * Agility.GetValue()) + 0.5f)); // Store the coroutine reference
                    yield return _currentMovementCoroutine; // Wait for the movement to complete
                }
            }

            IsMoving = false;
            animator.SetBool(Moving, IsMoving);
            MenuManager.Instance.DeFocusToTile();
        }

        // Move to a specific position (not so good)
        private IEnumerator MoveTo(Vector2 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y, startPosition.z); // Maintain z-coordinate
            float elapsed = 0;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;  // Wait until the next frame
            }

            transform.position = endPosition; // Ensure the unit exactly reaches the target position
        }
        
        
        // Move to a specific tile (much better)
        public IEnumerator MoveToTile(Tile targetTile, float baseDuration)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = targetTile.transform.position;
            bool isDiagonal = Mathf.Abs(targetTile.transform.position.x - startPosition.x) == Mathf.Abs(targetTile.transform.position.y - startPosition.y);
            float duration = isDiagonal ? baseDuration * 1.414f : baseDuration; // Diagonal movements are longer by the factor of sqrt(2)
            float elapsed = 0;

            while (elapsed < duration)
            {
                // PAUSE CONTROL
                if (GameManager.Instance.GameState == GameState.Inventory || GameManager.Instance.GameState == GameState.Pause)
                {
                    yield return new WaitWhile(() => GameManager.Instance.GameState == GameState.Inventory || GameManager.Instance.GameState == GameState.Pause);
                }
                

                transform.position = Vector3.MoveTowards(startPosition, endPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            if (OccupiedTile != null) OccupiedTile.OccupiedUnit = null;
            OccupiedTile = targetTile;
            targetTile.OccupiedUnit = this;
            //IsMoving = false;
        }
        
        public IEnumerator StopMovement()
        {
            if (IsMoving)
            {
                if (_currentMovementCoroutine != null)
                {
                    StopCoroutine(_currentMovementCoroutine);
                    _currentMovementCoroutine = null;
                }

                yield return StartCoroutine(MoveToTile(OccupiedTile, 0.5f));
                
                IsMoving = false;
                MenuManager.Instance.DeFocusToTile();
            }

        }
        
        
        public virtual int Attack(BaseUnit target)
        {
            // Implement generic attack logic here
            return target.TakeDamage((int)(Strength.GetValue() * 1.5f + Power.GetValue() * 1.5f + Focus.GetValue() * 1.5f));
            //target.CurrentHealth -= this.Strength.GetValue() - target.Armor.GetValue();
            //target.DeathControl();
            // Example: target.Health -= this.Strength * this.weapon.damage - target.Armor;
        }
        
        public int TakeDamage(int damage)
        {
            damage -= Armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            
            CurrentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage.");
            DeathControl();

            return damage;

        }


        public void Heal(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public bool DeathControl()
        {
            Debug.Log(this.UnitName + " Health: " + CurrentHealth);
            if (CurrentHealth <= 0)
            {
                Debug.Log(this.UnitName + " is dead");
                return true;
                Destroy(this);
                Die();
            }

            return false;
        }

        public virtual void Die()
        {
            Debug.Log(transform.name + " died.");
            
        }


    }
}