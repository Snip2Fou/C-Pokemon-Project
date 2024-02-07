using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Security.Policy;

public class Pokemon
{
    public string Name { get; set; }
    public string TypeOne { get; set; }
    public string TypeTwo { get; set; }
    public int Total { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int AttackSpecial { get; set; }
    public int DefenseSpecial { get; set; }
    public int Speed { get; set; }
    public int Level { get; set; }
    public int PvMax {  get; set; }
    public int Pv { get; set; }
    public int Xp { get; set; }
    public Capacity Capacity1 { get; set; }
    public Capacity Capacity2 { get; set; }
    public Capacity Capacity3 { get; set; }
    public bool IsUsing { get; set; }
    public bool Switch {  get; set; }



    public Pokemon(string name, string typeOne, string typeTwo, int total ,int health, int attack, int defense, int attackspecial, int defensespecial, int speed)
    {
        Name = name;
        TypeOne = typeOne;
        TypeTwo = typeTwo;
        Total = total;
        Health = health;
        Attack = attack;
        Defense = defense;
        AttackSpecial = attackspecial;
        DefenseSpecial = defensespecial;
        PvMax = GetPvByFormule();
        Pv = GetPvByFormule();
        Speed = speed;
        Level = 1;
        Xp = 0;
    }

    public Pokemon() {
        Level = 1;
        Capacity1 = null; Capacity2 = null; Capacity3 = null;
    }


    public Pokemon(string name)
    {
        Name = name;
        Level = 1;
    }

    public void TakeDamage(int damage)
    {
        Pv -= damage;
        if (Pv < 0)
        {
            Pv = 0;
        }
    }

    public void Heal(double heal)
    {
        if(Pv + heal > PvMax) 
        {
            Pv = PvMax;
        }
        else
        {
            Pv += (int)heal;
        }
    }

    public void GiveXp(Pokemon pokemon, bool capture)
    {
        if (capture)
        {
            int formuleXp = (int)(((1.5 * 8 * pokemon.Level) / (5 * 1)) * ((2 * pokemon.Level + 10) / Math.Pow((pokemon.Level + Level + 10), 2.5) + 1));
            if (Switch)
            {
                formuleXp = (int)(formuleXp * 1.5);
                Xp += formuleXp;
            }
            Xp += formuleXp;
        }
        else
        {
            int formuleXp = (int)(((1 * 8 * pokemon.Level) / (5 * 1)) * ((2 * pokemon.Level + 10) / Math.Pow((pokemon.Level + Level + 10), 2.5) + 1));
            if (Switch)
            {
                formuleXp += (int)(formuleXp * 1.5);
                Xp += formuleXp;
            }
            Xp += formuleXp;
        }

    }

    public void CanLevelUp()
    {
        if (Xp >= Math.Pow((Level+1),3))
        {
            Xp -= (int)Math.Pow((Level+1),3);
            Level += 1;
        } 
    }

    public bool IsAlive()
    {
        if(Pv > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetPvByFormule()
    {
        int pv = ((2 * Health * Level / 100) + Level + 10);
        return pv;
    }

    public int GetStatByFormule(int statistique)
    {
        int resultat_stat = (((2 * statistique * Level / 100) + 5) * 1);
        return resultat_stat;
    }

    public void CreateCapacity(string[] values, List<string> type_list)
    {
        string _name = values[1];
        string _des = "";
        int k = 2;
        while (!type_list.Contains(values[k].ToLower()))
        {
            _des += values[k];
            k++;
        }
        string type = values[k];
        string category = values[k+1];
        string power = values[k+2];
        if (power == "—")
        {
            power = "0";
        }
        string accuracy = values[k+3];
        if (accuracy == "—")
        {
            accuracy = "0";
        }
        else
        {
            string new_val = "";
            for (int i = 0; i < values[k+3].Length; i++) {
                if (values[k+3][i] == '%')
                {
                    break;
                }
                else
                {
                    new_val += values[k+3][i];
                }
            }
            accuracy = new_val;
        }
        bool critical = false;
        if (values[k+7] == "1")
        {
            critical = true;
        }

        if (Capacity1 == null)
        {
            Capacity1 = new Capacity
            {
                Name = _name,
                Description = _des,
                Type = type,
                Category = category,
                Power = int.Parse(power),
                Accuracy = int.Parse(accuracy),
                Critical = critical
            };
        }
        else if(Capacity2 == null)
        {
            Capacity2 = new Capacity
            {
                Name = _name,
                Description = _des,
                Type = type,
                Category = category,
                Power = int.Parse(power),
                Accuracy = int.Parse(accuracy),
                Critical = critical
            };
        }
        else if(Capacity3 == null)
        {
            Capacity3 = new Capacity
            {
                Name = _name,
                Description = _des,
                Type = type,
                Category = category,
                Power = int.Parse(power),
                Accuracy = int.Parse(accuracy),
                Critical = critical
            };
        }

    }
}