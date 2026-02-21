# Objekty
## GameObject
  - prázdný kontainer
  - **nic nedělá**
  - transform 3D
    - pozice
    - rotace
    - scale - velikost
  - tag
  - layer
  - jméno
  - dá se vypnout
  - může být větveno

```
    - root
      - child I
          - child child I
      - child II
      - child III
          - child child I
```
<br/>

## Component
``` C#
public class DownMover : MonoBehaviour
{

    void Start()
    {
    
    }

    void Update()
    {
    
    }
}
```
  - skript co dědí z MonoBehavior
  - přidává chování na [GameObject](#gameobject)
  - každý komponent by měl řešit jenom jednu věc
  - nemá constructor
<br/>

## Scene
  - skupina [GameObject](#gameobject)ů
  - uloženo na disku
  - může jich být víc aktivních najednou
<br/>

## Input Action Asset
  - soubor s ActionMapami
    - ActionMap je skupina [InputAction](#input-action)ů
    - pokud se vypne/zapne mapa
      -> vypne/zapne všechny přiřazené [InputAction](#input-action)
<br/>

## Input Action
  - jedna akce může mít více vazeb (wsad, ↑↓←→, joystick)
  - začíná vypnutý
    - **!!! Je nutné ho vždy nejdříve zapnout !!!**
  - typ akce
    - button - jednorázová akce (skok, střílení)
    - value - dlouhodobá akce (pohyb[vector2], koukání[vector2])
<br/>

<br/><br/><br/><br/><br/>

# Unity Messages
 
``` C#
void Awake(){}
```

## Awake
  - Inicializace
    - Nabírání referencí

<br/>

## Update
  - Každý frame
  - Nepoužívat těžké výpočty => špatný výkon
    - čtení user Inputu
    - posouvání

<br/>

## Start
  - Druhá inicializace
    - může používat async
    - Načítání souborů z disku

<br/>

## FixedUpdate
  - Periodický jev (základ je 20ms = 50 za sekundu)
    - používá se na fyziku

<br/>

## OnEnable
  - když se Component zapne

<br/>

## OnDisable
  - když se Component vypne

<br/>

### Colliders
  - 2d a 3d verze
  - collision a trigger verze
  - Enter
    - Když se collider střetne s jiným 
  - Exit
    - Když collider přestane dotýkat jiného
  - Stay
    - Každý frame co se collidery dotýkají  

  - `OnTriggerEnter2D()`, `OnCollisionExit()`, ...

<br/><br/><br/><br/><br/>

# Kód
## Debugování
  ``` C# 
    Debug.Log("zpráva");        // Zpráva do console (normální)
    Debug.LogWarning("zpráva"); // zpráva do console (warning)
    Debug.LogError("zpráva");   // zpráva do console (error)
    Debug.Break();              // pauzne hru
  ```

<br/>

## SerializeField
  ``` C#
    [SerializeField] float speed = 5; // stejné
    public float test = 5;
  ```
  - Vystavuje pole v inspectoru
  - Používá se pouze pro private/protected
   - public ho mají automaticky
  - Serializuje (ukládá) hodnotu pole
  - Může být změněno behěm hraní
    - Ale po ukončení hraní se neuloží
  - Nefunguje s typy:
    - static, const, readonly
    - Dictionary
    - Více dimensionální pole/listy
    - Interfaces, Generics

<br/>

## Vector
  - 2D(x,y) a 3D(x,y,z)
  - drží hodnoty pro osy

  ``` C#
    Vector2 a = Vector2.MoveTowards(new(0,2), new(2, 4), 1)
  ```
  - Vector.MoveTowards(Vector start, Vector destination, float maxMove)
    - vrací vector posunutý od startu do cíle
    - má maximální délku o kterou se může pohnout

<br/>

## Transform
### Position

``` C#
    Vector3 vec = new (0, 1, 2);
    transform.position = vec;
    transform.localPosition = vec;
    transform.Translate(vec);
  ```

  - transform.position
    - absolutní pozice objektu
  
  - transform.localPosition
    - relativní pozice od parent objektu
  
  - transform.Translate(Vector3 vec)
    - pohne pomocí vec
    - relativně k rotaci objektu

### Rotation
  ``` C#
    Vector3 vec = new (0, 1, 2);

    transform.rotation = Quaternion.Euler(vec);
    transform.localRotation = Quaternion.Euler(vec);
  ```
  - rotace není Vector ale Quaternion
    - komplikovaná hodnota (x, y, z, **w**)
      - šilené výpočty: https://en.wikipedia.org/wiki/Quaternion
    - pro jednodušší použití ``Quaternion.Euler(x, y, z)``
      - rotace čistě podle os

  - transform.rotation
    - absolutní rotace
  
  - transform.localRotation
    - relativní rotace od parent objektu

### Scale
  ``` C#
    Vector3 vec = new (0, 1, 2);

    transform.scale = vec;
    transform.localScale = vec;
  ```
  
  - udává poměr zvětšení oproti normálu
    - bez [component](#component)ů nic neznamená
  
  - transform.scale
    - absolutní zvětšení

  - transform.localScale
    - relativní zvětšení od parent objektu

<br/>

## Čtení inputu
  ``` C#
  [SerializeField] InputActionAsset inputActions; // 1. získání reference na `InputActionAsset`, naplnit v  inspectoru!
  InputAction move;

  void Awake()
  {
      move = inputActions.FindAction("Move"); // 2. získání akce ze souboru
      
      move.Enable(); // 3. zapnutí akce
  }

  void Update()
  {
    Vector3 vec = move.ReadValue<Vector2>(); // 4. Přečtení hodnoty akce
    // D => (1,  0)
    // W => (0,  1)
    // _ => (0,  0)
    // S => (-1, 0)
  }
  ```
  - Na čtení inputu si nejdříve musíme vytvořit soubor s nadefinovanými akcemi a jejich vazbami
    - který poté předáme kódu, a vybereme z něj jednotlivé akce
  - předtím než začneme cokoliv číst musíme akci zapnout
    - **BEST PRACTICE**: používání OnEnable, OnDisable; pro zapínání/vypínání
      - pokud někde používáme action.performed
      - a daná věc se zničí/odnačte bude to dělat problémy(využívání neexistujícího objektu => error)     

<br/>

## Fyzika
  - 2d, 3d verze
  - počítá se ve [FixedUpdate](#fixedupdate)

### Collider
  - zóna detekující kolize s ostatními Collidery
    - minimálně jeden z kolidujících objektů musí mít RigidBody
  - váže se na RigidBody
    - 1 RigidBody - x Colliderů
  - různé tvary
  - může být nastavený jako `trigger`
    - není fyzický (dostává eventy, ale neblokuje pohyb)
  
<br/>


### RigidBody
  - dělá z objektu (a všech jeho dětí) fyzický objekt
    - bez colliderů nezná jak velký je
    - nepoužívat vnořené RigidBody, vede k desynchronizaci
  - dynamický
    - síla a fyzika mají efekt
  - kinematický
    - fyzika nemá efekt
    - dá se hýbat pouze přímo z kódu
  
  - síla
    - `AddForce()`
    - sama se neztrácí (pohybové zákony)
  ``` C#
  [SerializeField] Rigidbody rb;
  
  void FixedUpdate(){
    Vector3 vec = new(0,1,2);

    rb.MovePosition(vec); // přesune na novou pozici

    rb.AddForce(vec); // "šťouchne" do objektu silou
  }
  ```
