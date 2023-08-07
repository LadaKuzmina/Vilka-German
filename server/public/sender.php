<?php
    if(isset($_POST['username']){
    $name = $_POST['username'];
    }
    if(isset($_POST['phone']){
	$phone = $_POST['phone'];
	}
	if(isset($_POST['email']){
    $email = $_POST['email'];
    }

// 	$to = "lada.kuzmina2003@gmail.com";
// 	$from = $email;
// 	$subject = "Заявка c сайта";

	$headers  = 'MIME-Version: 1.0' . "\r\n";
    $headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";


// 	$msg='Имя:'.$name."\n"
//     Имя: $name /n
//     Телефон: $phone /n
//     Почта: $email /n
   if(mail("lada.kuzmina2003@gmail.com", "Заявка с сайта",
     'Имя: '.$name."\n".
     'Телефон: '.$phone"\n".
     'Почта: ' $email)){
     echo('Успешно отправлено');
     } else{
     echo('Ошибка');}
// 	mail($to, $subject, $msg);

?>