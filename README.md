<h1>Food Delivery - ASP.NET MVC Application</h1>

<p>Этот проект представляет собой веб-приложение для доставки еды, созданное с использованием ASP.NET MVC. В отдельный микро-сервис вынесен функционал по работе с отзывами о товарах, ознакомиться с ним можно <a href="https://github.com/GeorgeD615/ReviewWebApi" target="_blank">здесь</a>.</p>

<h2>Функциональность</h2>
<h3>Пользователь</h3>
<ul>
  <li>Регистрация и авторизация</li>
  <li>Просмотр доступных блюд, поиск по названию</li>
  <li>Добавление блюд в корзину и в список избранного</li>
  <li>Оформление заказа</li>
  <li>Просмотр истории заказов</li>
  <li>Отправление отзывов о блюдах</li>
  <li>Смена логина и установка аватра</li>
</ul>
<h3>Администартор</h3>
<ul>
  <li>Просмотр заказов, изменение их статуса</li>
  <li>Создание, редактирование и удаление товаров</li>
  <li>Создание и удаление ролей</li>
  <li>Создание, удаление и редактирование пользователей</li>
</ul>

<h2>Стек технологий</h2>
<ul>
  <li><strong>Back-end:</strong> ASP.NET MVC (C#)</li>
  <li><strong>Front-end:</strong> HTML5, CSS3, Bootstrap</li>
  <li><strong>База данных:</strong> Microsoft SQL Server</li>
  <li><strong>ORM:</strong> Entity Framework</li>
  <li><strong>Аутентификация:</strong> ASP.NET Identity</li>
  <li><strong>Контейнеризация:</strong> Docker</li>
</ul>

<h2>Требования</h2>
<p>Для развёртывания проекта убедитесь, что у вас установлен и запущен docker-engine:</p>
<ul>
    <li><a href="https://www.docker.com/get-started" target="_blank">Docker</a></li>
</ul>

<h2>Установка</h2>

<h3>Шаг 1: Клонирование репозитория</h3>
<pre><code>git clone https://github.com/GeorgeD615/DeliveryWebApp.git
cd DeliveryWebApp/OnlineShop
</code></pre>

<h3>Шаг 2: Запуск с использованием Docker Compose</h3>
<p>Запустите сервисы с помощью команды:</p>
<pre><code>docker-compose up -d</code></pre>
<p>Приложение будет доступно по адресу <a href="http://localhost:80" target="_blank">http://localhost:80</a>.</p>

<p>Докер образ приложения размещён <a href="https://hub.docker.com/r/georged615/online_shop_web_app" target="_blank">здесь</a>.</p>

<h2>Основные страницы</h2>
<ul>
  <li><strong>Главная страница:</strong></li>
  <li><strong>Личный кабинет:</strong></li>
  <li><strong>Страница блюда:</strong></li>
  <li><strong>Корзина:</strong></li>
  <li><strong>Админ панель (страница заказов):</strong></li>
</ul>
