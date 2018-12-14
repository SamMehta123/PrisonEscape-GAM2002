using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityStandardAssets.CrossPlatformInput;
//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MovingObject
{
    public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
    public int pointsPerCoin = 10;              //Number of points to add to player Coin points when picking up a Coin object.
    public Text CoinText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip CoinSound1;
    public AudioClip CoinSound2;
    public AudioClip gameOverSound;

    private Animator animator;                  //Used to store a reference to the Player's animator component.
    private int Coin;                           //Used to store player Coin points total during level.
    private int Wall;
    private Vector3 RespawnPoint = new Vector3(6.63f, 0.42f, 0);

    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.

    //Start overrides the Start function of MovingObject
    protected override void Start()
    {

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        Coin = GameManager.instance.playerCoinPoints = playerHealth.currentCoin;

        CoinText.text = "Coins: " + Coin;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        base.Start();
    }

    private void Update()
    {
        //If it's not the player's turn, exit the function.
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

            
        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }


        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            Coin = GameManager.instance.playerCoinPoints = playerHealth.currentCoin;
            CoinText.text = "+" + pointsPerCoin + " Coin: " + playerHealth.currentCoin;
        }

        if (playerHealth.currentCoin <= -1)
        {
            playerHealth.currentCoin = 0;
            gameObject.transform.position = RespawnPoint;
            //go to room 1
        }
    }

    //AttemptMove overrides the AttemptMove function in the base class MovingObject
    //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        //Every time player moves, subtract from Coin points total.
        playerHealth.currentCoin--;
        //Coin = GameManager.instance.playerCoinPoints = playerHealth.currentCoin;
        //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
        base.AttemptMove<T>(xDir, yDir);

        //Hit allows us to reference the result of the Linecast done in Move.
        RaycastHit2D hit;

        //If Move returns true, meaning Player was able to move into an empty space.
        if (Move(xDir, yDir, out hit))
        {
            //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }

        //Since the player has moved and lost Coin points, check if the game has ended.
        CheckIfGameOver();

        //Set the playersTurn boolean of GameManager to false now that players turn is over.
        GameManager.instance.playersTurn = false;
    }


    //This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable()
    {
        //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
        GameManager.instance.playerCoinPoints = Coin;
    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
           
            enabled = false;
        }

        if (other.gameObject.tag == "Coin")
        {

            playerHealth.currentCoin += pointsPerCoin;
            //Coin += pointsPerCoin;
            Coin = GameManager.instance.playerCoinPoints = playerHealth.currentCoin;
            SoundManager.instance.RandomizeSfx(CoinSound1, CoinSound2);
            CoinText.text = "+" + pointsPerCoin + " Coin: " + Coin;
           // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

       
    }


    //Restart reloads the scene when called.
    private void Restart()
    {
        //Load the last scene loaded, in this case Main, the only scene in the game.
        SceneManager.LoadScene(0);
    }


    //LoseFood is called when an enemy attacks the player.
    //It takes a parameter loss which specifies how many points to lose.
    public void LoseCoin(int loss)
    {
        //Set the trigger for the player animator to transition to the playerHit animation.
        animator.SetTrigger("GaurdAttack");

        //Subtract lost Coin points from the players total.
        Coin -= loss;
        CoinText.text = "-" + loss + " Coin: " + Coin;

        //Check to see if game has ended.
        CheckIfGameOver();
    }


    //CheckIfGameOver checks if the player is out of Coin points and if so, ends the game.
    private void CheckIfGameOver()
    {
        //Check if food point total is less than or equal to zero.
        if (Coin <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            //Call the GameOver function of GameManager.
            GameManager.instance.GameOver();
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        throw new NotImplementedException();
    }
}