package user;

import java.util.ArrayList;
import java.util.Scanner;

public class userData {
    private ArrayList<String> UserName = new ArrayList<>();
   private ArrayList<String> password = new ArrayList<>();
    String userT;
    String scannerT;
    public void InputUserName(String Obj){ 
        int conCheck = 0;
        while(true){
        
        try (Scanner scanner = new Scanner(System.in)) { // quick fix idk what try does
            scannerT = scanner.next(); // change to Obj if username is recieved elsewhere
        }
        for(int i =0; i<UserName.size(); ++i){
            userT = UserName.get(i);
            if(scannerT==userT){
                System.out.print("Enter unique UserName");
                break;
            }
            conCheck=1;
        }
        if(conCheck==1){
        UserName.add(scannerT); // should break with unique username
        break;
        }
    }
    
    }
    public void InputPassWord(String username, String Obj){
          int check1 =0;
        int check2 = 0;
        int check3 = 0;
        int check4 = 0;
        while(true){
        try (Scanner scanner = new Scanner(System.in)) { // quick fix idk what try does
            scannerT = scanner.next(); // change to Obj if password is recieved elsewhere
        } // idk if i want this defined outside of loop or inside 
         check1 =0;
         check2 = 0;
         check3 = 0;
         check4 = 0;
        for(int i =0; i<scannerT.length(); ++i){
            if(scannerT.charAt(i) == 0) // improve for all numbers
            {
                check1 = 1;
            }
            if(scannerT.charAt(i) == 1){ // check for symbols
                check2 = 1;
            }
            if(scannerT.charAt(i) >0) // check for capitol
            {
                check3 = 1;

            }
            if(scannerT.charAt(i) >0) // check for something
            {
                check4 = 1;

            }

        }
        if(check1==1&&check2==1&&check3==1&&check4==1){
             System.out.print("Password accepted");
            password.add(scannerT); // no password should be added without username
       break;
        }//might want error check though
        else{
            System.out.print("Password needs a capitol, lowercase, number, and symbol");
        }
    }
}
}
