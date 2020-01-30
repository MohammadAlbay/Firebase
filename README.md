# Firebase
Firebase realtime library for c# using rest API

Simple to use with a few lines

DatabaseReference database = FirebaseReference.GetReference("project_name").GetDatabaseReference();
// like android's firebase SDK
DatabaseReference child = database.Child("child");
// to get data
FirebaseEventHandler get = child.Get();
get.OnSuccess = (result) => {
    /* whatever */
};
get.OnFail = (exception) => {
   /*  thow it! */
}


