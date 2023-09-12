import java.util.ArrayList;

public class List1
 {
    private int[] price = new int[10];
    private int count;
    private ArrayList<String> itemList = new ArrayList<>();
    public void add(int item) { // called with the item price
        price[count] = item;
    }
    public void addItemToList(String Obj){ // this is called with the name of the item
        itemList[count] = Obj; //investigate arrayList<strings> 
    }
    public int get(String Name){ 
        for(int i =0; i<count; ++i){
            if(itemList[i] == Name){
                return price[i];
                break;
            }
        }
        return 0;
    }
}
