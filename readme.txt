edit by dwq 2015-07-24

当前已实现的功能：

1.对TextBox内容的验证（包括实数型，整数型和角度型验证，支持实数和整数类型的科学计数法输入）,可以指定添加验证到（TextChange或者Leave，由eventtype指定）；

2.对DataGridView内容的验证，对其各列数据的验证，以及指定数据列非空的判断（notnullindex指定非空列索引，逗号分隔）；

3.对于按钮的验证，一个按钮对应几个数据控件（TextBox和DataGridView），按钮Click事件中调用DataCheck的

DataValidate静态方法，返回一个bool值，指示是否所有必填项已经有内容（调用DataValidate方法之前，确保已经设

置了XML文件路径)

使用注意：

1.当前数据验证只能绑定当前类中的控件，即使xml中存在其他类的控件，也是不予绑定的；

2.使用将本项目生成的dll文件拷贝到使用项目目录添加引用，验证之前需要先设置XML文件路径，验证入口函数为DataCheck的Check方法（需要的参数为当前窗体对象），非空验
证的入口为DataValidate，需要的参数为按钮实例；

3.对DataGridView进行数据绑定时，需要将其全部数据列添加类型限制（不进行限制的，以NULL或者UNLIMITED填充)；

对于XML文件的要求：

1.根节点名为parameters，根节点包含两个子节点（controls和types）；
	<?XML encoding="" ?>
	<parameters>
	   <controls>
		...
	   </controls>

	   <types>
		...
	   </types>
	   <tipmessages>
		...
	   </tipmessages>
	</parameters>

2.controls节点内容主要是控件信息（主要为三种TextBox，DataGridView和Button，TextBox需要的属性有datatype，eventtype；DataGridView主要属性有datatype，notnullindex；Button的属性有controls,tip），
节点层次为：
   <controls>
	<命名空间名1>
	   <类名1>
		<控件名1 datatype="类型名" eventtype="TEXTCHANGE">
		</控件名1>
	   </类名1>
	   <类名2>
		<控件名2 datatype="类型名" eventtype="LEAVE">
		</控件名2>
	   </类名2>
	   ...
	</命名空间名1>
	<命名空间名2>
	   <类名5>
		<控件名7 datatype="类型名" eventtype="TEXTCHANGE">
		</控件名7>
	   </类名5>
	</命名空间名2>
	...
   </controls>
控件的datatype属性值是types下某一子节点的名称；

对于Button和控件在同一级，同样位于命名空间-类-Button（注意：controls属性中的控件不需要一定在配置文件里，是通
过控件名直接检索当前页面里面同名的控件）

		<按钮名 controls="控件1,控件2,..." tip="tip1">

3.types节点主要为与控件对应的数据类型内容，组织形式如下：
	<types>
	   <type1 colIdx="0,1,2，..." datatype="REALNUMBER,NULL,NULL,..." />
	   <type2 colIdx="-1" datatype="REALNUMBER" />
	   ...
	</types>
关于type的新子节点的命名需唯一标识，colIdx存放数据的列索引（对于TextBox可不写），多个索引直接以逗号隔开，而datatype存放与colIdx对应的数据类型，多个类型之间以
逗号隔开，可用的数据类型有NULL或者UNLIMITED（此列无限制），REALNUMBER（实数型），INTEGER（整数型）和ANGLE（角度型）


4.tipmessages节点指示当按钮验证不通过时的错误提示信息，errorno属性为错误编号，多个错误编号间以逗号分隔，“0”表示验证成功，“1”表示验证失败；errormessage指
示错误提示信息，多个错误信息间用逗号分隔。如果只是自定义验证失败信息，则只需填写errormessage属性值即可
	<tipmessages>
	   <tip1 errorno="0,1" errormessage="验证成功,该按钮存在空置的必填项" />
	   <tip2 errormessage="该按钮存在空置的必填项" />
	   ...
	</tipmessages>