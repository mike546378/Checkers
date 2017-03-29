package gradedUnit.Backend.Checkers.MichaelReid;


import java.io.IOException;
import java.net.ServerSocket;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.ConcurrentHashMap;

public class Base {

	private static List<Connection> clients = Collections.synchronizedList(new ArrayList<Connection>());
	private static ConcurrentHashMap<UUID, Game> games = new ConcurrentHashMap<UUID, Game>();
	static ServerSocket serverSocket;
	
	
	/**
	 * Initilization
	 * @param args
	 */
	public static void main(String[] args) {
		System.out.println("---------------------------------------------------");
		System.out.println("-------------------CHECKERS SERVER-----------------");
		System.out.println("---------------------------------------------------");

		int port = 5463;		
		try {
			serverSocket = new ServerSocket(port); //Creating new server socket	
			heartbeat(); //Starts heartbeat thread
			while(true)
			{
				try {
					System.out.println("Listening for connections");
					clients.add(new Connection(serverSocket.accept()));
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
			
		} catch (IOException e1) {
			e1.printStackTrace();
		}		
	}
	
	
	/**
	 * Deletes a game from memory
	 * @param gameID The id of the game to be deleted
	 */
	public static void delGame(UUID gameID)
	{
		games.remove(gameID);
	}
	
	/**
	 * Heartbeat thread to periodically flush out old connections
	 */
	private static void heartbeat()
	{
		Thread beat = new Thread(new Runnable(){
			@Override
			public void run()
			{
				boolean removed = false;
				while(true)
				{
					removed = false;
					synchronized(clients){
						Iterator<Connection> i = clients.iterator();
						while(i.hasNext())
						{
							Connection c = i.next();
							if(!c.send("heartbeat"))
							{
								clients.remove(c);
								removed = true;
								System.out.println("Removed old connection");
								break;
							}
						}
					}
					if(!removed)
						matchmake();
						try {
							Thread.sleep(5000); //Heartbeat every 5 seconds
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
				}	
			}});
		beat.start();
	}
	
	
	/**
	 * Method to matchmake active connections
	 */
	private static void matchmake()
	{
		ArrayList<Connection> freeConnections = new ArrayList<Connection>();
		Iterator<Connection> i = clients.iterator();
		while(i.hasNext())
		{
			Connection c = i.next();
			if(c.getGameUUID() == null && !c.isBusy())
				freeConnections.add(c);
		}
		if(freeConnections.size() >= 2)
		{
			System.out.println("Attempting to matchmake with " + freeConnections.size() + " free connections");
			int matches = (int) Math.floor(freeConnections.size() / 2);
			System.out.println("Matches: " + matches);
			for(int x=0; x<matches;x++) //Loop through all avaliable pairs of connections, sending the connection string
			{
				UUID gameID = UUID.randomUUID();
				createGame(gameID);
				freeConnections.get(x*2).setBusy(true);
				freeConnections.get(x*2).send("team=white");
				freeConnections.get(x*2).send("connect="+gameID);
				freeConnections.get(x*2+1).setBusy(true);
				freeConnections.get(x*2+1).send("team=dark");
				freeConnections.get(x*2+1).send("connect="+gameID);
				System.out.println("Attempting matchmaking for game " + gameID);
			}
		}
	}
	
	/**
	 * Sends a message to all clients
	 * @param msg
	 */
	@SuppressWarnings("unused")
	private static void broadcast(String msg)
	{
		System.out.println("-----------Starting Broadcast--------------");
		for(Connection c : clients)
			c.send(msg);
	}

	
	/**
	 * Creats new Game instance with given UUID
	 * @param id
	 */
	public static void createGame(UUID id)
	{
		System.out.println("Created new game with ID " + id);
		games.putIfAbsent(id, new Game(id));
	}
	
	
	/**
	 * Adds a client to a game instance
	 * @param conn
	 * @param gameID
	 */
	public static boolean addToGame(Connection conn, UUID gameID, String team)
	{
		Game game = games.get(gameID);
		if(game != null){
			game.addPlayer(conn, team);
			System.out.println("Added " + conn.getClientUUID() + " To game " + gameID);
			return true;
		}
		return false;
	}
	
	
	/**
	 * Returns the game associated with specified ID
	 * @param id
	 * @return
	 */
	public static Game getGame(UUID id)
	{
		return games.get(id);
	}
	
	
	/**
	 * Removes a connnection from the list
	 * @param connection
	 */
	public static void removeConn(Connection connection) {
		clients.remove(connection);
	}
}
