using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private float maxSpeed = 300.0f; // Maximale Geschwindigkeit
    private float acceleration = 190.0f; // Wert, um die Geschwindigkeit zu erhöhen
    private float deceleration = 30.0f; // Wert, um die Geschwindigkeit zu verringern
    private string PlayerDirection = "u";
    private string PlayerAction = "Idle_";
    private AnimationPlayer animationPlayer;
    private Sprite2D sprite2D;
    private Texture2D idleTexture;
    private Texture2D runTexture;
     



    public override void _Ready()
    {
        // AnimationPlayer finden
        animationPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        sprite2D = GetNodeOrNull<Sprite2D>("Sprite2D");
        idleTexture = GD.Load<Texture2D>("res://Sprites/Player/Player_idle.webp");
        runTexture = GD.Load<Texture2D>("res://Sprites/Player/Player_run.webp");
    }

    public override void _Process(double delta)
    {
        // Bewegung und Richtung berechnen
        string calculateForce = CalculateForce(InputActions());
        
        if (calculateForce != "fire" && calculateForce != "interact")
        {
            PlayerDirection = calculateForce;
        }
        
        // Animation anpassen
        PlayerAnimation();
    }

    // Methode zur Steuerung der Animation
    public void PlayerAnimation()
    {
        if (animationPlayer == null)
            return;

        string setAnimation = PlayerAction + PlayerDirection;
        if (animationPlayer.CurrentAnimation != setAnimation)
        {
            animationPlayer.Play(setAnimation);
            
        }
    }

    // Methode zur Verarbeitung der Eingaben und Berechnung der Bewegung
    public string InputActions()
    {
        Vector2 velocity = Vector2.Zero;
        string tryaction = PlayerDirection;

        // Bewegungsrichtung und Geschwindigkeit direkt in den bestehenden Blöcken festlegen
        if (Input.IsActionPressed("fire"))
        {
            tryaction = "fire";
        }
        else if (Input.IsActionPressed("interact"))
        {
            tryaction = "interact";
        }
        else if (Input.IsActionPressed("moveUp"))
        {
            if (Input.IsActionPressed("moveLeft"))
            {
                tryaction = "lb";
                velocity = new Vector2(-1, -1).Normalized() * maxSpeed;
            }
            else if (Input.IsActionPressed("moveRight"))
            {
                tryaction = "rb";
                velocity = new Vector2(1, -1).Normalized() * maxSpeed;
            }
            else
            {
                tryaction = "b";
                velocity = new Vector2(0, -1).Normalized() * maxSpeed;
            }
        }
        else if (Input.IsActionPressed("moveDown"))
        {
            if (Input.IsActionPressed("moveLeft"))
            {
                tryaction = "lu";
                velocity = new Vector2(-1, 1).Normalized() * maxSpeed;
            }
            else if (Input.IsActionPressed("moveRight"))
            {
                tryaction = "ru";
                velocity = new Vector2(1, 1).Normalized() * maxSpeed;
            }
            else
            {
                tryaction = "u";
                velocity = new Vector2(0, 1).Normalized() * maxSpeed;
            }
        }
        else if (Input.IsActionPressed("moveLeft"))
        {
            tryaction = "l";
            velocity = new Vector2(-1, 0).Normalized() * maxSpeed;
        }
        else if (Input.IsActionPressed("moveRight"))
        {
            tryaction = "r";
            velocity = new Vector2(1, 0).Normalized() * maxSpeed;
        }

        // Bewegung auf CharacterBody2D anwenden
        Velocity = velocity;
        MoveAndSlide();

        return tryaction;
    }

    // Berechnet die endgültige Richtung oder Aktion des Spielers
    public string CalculateForce(string tryaction)
    {
        if (tryaction == "fire" || tryaction == "interact")
        {
            return tryaction;
        }
        else
        {
            if (Velocity.Length() == 0) // Spieler steht still (z.B. bei Kollision)
            {
                PlayerAction = "Idle_";
                sprite2D.Texture = idleTexture;
                if (tryaction == "lu" || tryaction == "ru")
                {
                    tryaction = "u";
                    return tryaction;
                }
                else if (tryaction == "rb" || tryaction == "lb")
                {
                    tryaction = "b";
                    return tryaction;
                }
            }
            else{
                PlayerAction = "run_";
                sprite2D.Texture = runTexture;
                return tryaction;
            }
            return tryaction;
        }
    }
}
