package gradedUnit.Backend.Checkers.MichaelReid;

import java.util.HashMap;
import java.util.UUID;

public class Game {

	private UUID gameID;
	private HashMap<String, Connection> players = new HashMap<String, Connection>();
	
	
	/**
	 * Constructor
	 */
	public Game(UUID id)
	{
		gameID = id;
	}
	
	/**
	 * Add player to the game
	 * @param conn
	 * @return
	 */
	public boolean addPlayer(Connection conn, String team)
	{
		players.put(team, conn);
		System.out.println("Player added to team " + team);
		System.out.println("Playercount in game: " + players.size());
		if(players.size() < 2)
		{
			conn.send("status=waiting");
		}else{
			startGame();
		}
		return true;
	}
	
	/**
	 * Relays a message from one player to the other
	 * @return
	 */
	public boolean relay(String msg, Connection sender)
	{
		if(players.get("black") == sender){
			return players.get("white").send(msg);
		}else{
			return players.get("black").send(msg);}
	}
	
	
	/**
	 * Starts the game
	 */
	public void startGame()
	{
		broadcast("status=start");
		broadcast("turn=black");
 	}
	
	
	/**
	 * Sends message to both clients
	 */
	public void broadcast(String msg)
	{
		for(Connection c : players.values())
			c.send(msg);
	}
	
	/**
	 * Returns the games ID
	 * @return
	 */
	public UUID getGameID()
	{
		return gameID;
	}
}
