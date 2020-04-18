# Firebase
Firebase realtime library for c# using rest API

**Simple to use with a few lines**

    DatabaseReference database = FirebaseReference.GetReference("project_name").GetDatabaseReference();

**move to child**

    DatabaseReference child = database.Child("child");


**Retrieving data**

    FirebaseEventHandler get = child.Get(RetrievedDataType.JSON);
    get.Timeout = 10000;
    get.OnSuccess = (result) => {
        /* whatever */
    };

    get.OnFail = (exception) => {
        /*  throw it! */
    };
    // or 
    get.OnComplete = (firebase_operation) => {
        // firebase_operation.OperationState, true, false
        // firebase_operation.OperationResult, string, null
        // firebase_operation.ThrownException, Exception, null
    };
    get.Start(); /* try StartAsync() instead */


**Add data with unique key**

    FirebaseEventHandler post = child.Post(new { MyName = "Mohammad", Age = 22, I_Love = "C#"  });
    /* the same event-handling way as mentioned above */
    

**Add data without unique key**

    FirebaseEventHandler put = child.Put(new { MyName = "Mohammad", Age = 22, I_Love = "C#"  });
    /* the same event-handling way as mentioned above */
    
    
**Update**
    
    // 1.
    // erasing other nodes
    FirebaseEventHandler update = child.Update(new { expiration_date = "28/2/2020" });
    /* the same event-handling way as mentioned above */
    
    // 2.
    // updates only "expiration_date" without erasing other childs
    FirebaseEventHandler put = child.Child("expiration_date").Put("1/1/1899");
    /* the same event-handling way as mentioned above */
    
  
**Delete**

    FirebaseEventHandler delete = child.Child("expiration_date").Delete();
    /* the same event-handling way as mentioned above */
    /* note that OnSuccess(result) : if result is null this means HTTP 200 OK which means Done */

**How to check the exsistence of a child?**

    FirebaseEventHandler get = child.Child("UnknowChild").Get();
    get.OnSuccess = (result) => {
        if(result == null) {
            /* whatever */
        }
    };
    get.StartAsync();
