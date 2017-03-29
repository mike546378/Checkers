package gradedUnit.Backend.Checkers.MichaelReid;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
import java.util.UUID;

public class Connection {

	private Socket sock;
	private UUID gameUUID = null;
	private boolean busy = false;
	private UUID clientUUID = UUID.randomUUID();
	private Thread input;
	
	
	//Color coding for terminal output
	public static final String ANSI_RESET = "\u001B[0m";
	public static final String ANSI_BLACK = "\u001B[30m";
	public static final String ANSI_RED = "\u001B[31m";
	public static final String ANSI_GREEN = "\u001B[32m";
	public static final String ANSI_YELLOW = "\u001B[33m";
	public static final String ANSI_BLUE = "\u001B[34m";
	public static final String ANSI_PURPLE = "\u001B[35m";
	public static final String ANSI_CYAN = "\u001B[36m";
	public static final String ANSI_WHITE = "\u001B[37m";
	
	
	
	/**
	 * Initilization through constructor
	 * @param clientSocket
	 */
	public Connection(Socket clientSocket)
	{
		busy = true;
		
		sock = clientSocket;
		//Run getInput() method in seperate thread
		input = new Thread(new Runnable(){public void run(){getInput();}});
		input.start();
		//Allow matchmaking after a couple of seconds
		Thread delay = new Thread(new Runnable(){public void run(){try {Thread.sleep(1000);} catch (InterruptedException e) {} busy = false;}});
		delay.start();
	}
	
	/**
	 * Method used for retreiving input from client socket
	 */
	private void getInput()
	{
		BufferedReader inputStream = null;
		String input;
		try {
			inputStream = new BufferedReader(new InputStreamReader(sock.getInputStream()));
		} catch (IOException e1) { System.out.println(ANSI_YELLOW + "Error with input stream reader" + ANSI_RESET);}
		send("connected");
		//Continuously receive data from client
		while(true)
		{
			try {	
				System.out.println(ANSI_PURPLE + clientUUID + ": Waiting for input from client" + ANSI_RESET);
				input = inputStream.readLine();
				if(input.contains("CKSHND`")){
					System.out.println(ANSI_GREEN + clientUUID + ": Received:- " + input + ANSI_RESET);
					//Parsing of received message
					String[] msg = input.split("`");
					if(msg.length > 1)
					{
						//Check for initilization message
						if(msg[1].equalsIgnoreCase("init") && msg.length == 4)
						{
							if(msg[2] != "null" && msg[3] != null)
							{
								//Attempt to connect to game with given UUID
								if(!Base.addToGame(this, UUID.fromString(msg[2]), msg[3]))
								{
									send("error=Game not found");
								}else{
									setGameUUID(UUID.fromString(msg[2]));
								}
							}else{
								send("error=invalid initilization string");
							}
						}else if(msg[1].contains("relay="))
						{
							Base.getGame(gameUUID).relay(msg[1].substring(msg[1].indexOf("=")+1), this);
						}
					}
				}
			} catch (IOException e) {
				System.out.println(ANSI_PURPLE + clientUUID + ": Disconnected"+ ANSI_RESET);
				terminate();
				break;
			}
		}	
	}
	
	/**
	 * Cleanup when terminating connection
	 */
	public void terminate()
	{
		if(this.gameUUID != null){
			Base.getGame(gameUUID).broadcast("Disconnect");
			Base.delGame(gameUUID);
		}
		System.out.println(ANSI_PURPLE + clientUUID + ": Terminating connection" + ANSI_RESET);
		if(input.isAlive())
			input.interrupt();
		if(sock.isConnected())
			try {sock.close();} catch (IOException e){}
		Base.removeConn(this);
	}

	
	/**
	 * Sends message to client
	 */
	public boolean send(String msg)
	{
		msg = "CKSHND`" + msg + "|ET|";
		try{
			String prefix = ANSI_RED;
			if(!msg.contains("heartbeat"))
				System.out.println(prefix + clientUUID + ": Sending:- " + msg + ANSI_RESET);
			getSock().getOutputStream().write(msg.getBytes());
			return true;
		}catch(Exception ex)
		{
			System.out.println(ANSI_PURPLE + "Error sending msg to " + getSock().getRemoteSocketAddress() + ANSI_RESET);
			return false;
		}
	}
	
	
	/**
	 * @return the sock
	 */
	public Socket getSock() {
		return sock;
	}

	/**
	 * @param sock the sock to set
	 */
	public void setSock(Socket sock) {
		this.sock = sock;
	}

	/**
	 * Returns game uuid
	 * @return
	 */
	public UUID getGameUUID() {
		return gameUUID;
	}

	/**
	 * Sets Game UUID
	 * @param gameUUID
	 */
	public void setGameUUID(UUID gameUUID) {
		this.gameUUID = gameUUID;
	}

	/**
	 * @return the clientUUID
	 */
	public UUID getClientUUID() {
		return clientUUID;
	}

	/**
	 * @param clientUUID the clientUUID to set
	 */
	public void setClientUUID(UUID clientUUID) {
		this.clientUUID = clientUUID;
	}

	/**
	 * @return the busy
	 */
	public boolean isBusy() {
		return busy;
	}

	/**
	 * @param busy the busy to set
	 */
	public void setBusy(boolean busy) {
		this.busy = busy;
	}
}
