edit by dwq 2015-07-24

��ǰ��ʵ�ֵĹ��ܣ�

1.��TextBox���ݵ���֤������ʵ���ͣ������ͺͽǶ�����֤��֧��ʵ�����������͵Ŀ�ѧ���������룩,����ָ�������֤����TextChange����Leave����eventtypeָ������

2.��DataGridView���ݵ���֤������������ݵ���֤���Լ�ָ�������зǿյ��жϣ�notnullindexָ���ǿ������������ŷָ�����

3.���ڰ�ť����֤��һ����ť��Ӧ�������ݿؼ���TextBox��DataGridView������ťClick�¼��е���DataCheck��

DataValidate��̬����������һ��boolֵ��ָʾ�Ƿ����б������Ѿ������ݣ�����DataValidate����֮ǰ��ȷ���Ѿ���

����XML�ļ�·��)

ʹ��ע�⣺

1.��ǰ������ֻ֤�ܰ󶨵�ǰ���еĿؼ�����ʹxml�д���������Ŀؼ���Ҳ�ǲ���󶨵ģ�

2.ʹ�ý�����Ŀ���ɵ�dll�ļ�������ʹ����ĿĿ¼������ã���֤֮ǰ��Ҫ������XML�ļ�·������֤��ں���ΪDataCheck��Check��������Ҫ�Ĳ���Ϊ��ǰ������󣩣��ǿ���
֤�����ΪDataValidate����Ҫ�Ĳ���Ϊ��ťʵ����

3.��DataGridView�������ݰ�ʱ����Ҫ����ȫ������������������ƣ����������Ƶģ���NULL����UNLIMITED���)��

����XML�ļ���Ҫ��

1.���ڵ���Ϊparameters�����ڵ���������ӽڵ㣨controls��types����
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

2.controls�ڵ�������Ҫ�ǿؼ���Ϣ����ҪΪ����TextBox��DataGridView��Button��TextBox��Ҫ��������datatype��eventtype��DataGridView��Ҫ������datatype��notnullindex��Button��������controls,tip����
�ڵ���Ϊ��
   <controls>
	<�����ռ���1>
	   <����1>
		<�ؼ���1 datatype="������" eventtype="TEXTCHANGE">
		</�ؼ���1>
	   </����1>
	   <����2>
		<�ؼ���2 datatype="������" eventtype="LEAVE">
		</�ؼ���2>
	   </����2>
	   ...
	</�����ռ���1>
	<�����ռ���2>
	   <����5>
		<�ؼ���7 datatype="������" eventtype="TEXTCHANGE">
		</�ؼ���7>
	   </����5>
	</�����ռ���2>
	...
   </controls>
�ؼ���datatype����ֵ��types��ĳһ�ӽڵ�����ƣ�

����Button�Ϳؼ���ͬһ����ͬ��λ�������ռ�-��-Button��ע�⣺controls�����еĿؼ�����Ҫһ���������ļ����ͨ
���ؼ���ֱ�Ӽ�����ǰҳ������ͬ���Ŀؼ���

		<��ť�� controls="�ؼ�1,�ؼ�2,..." tip="tip1">

3.types�ڵ���ҪΪ��ؼ���Ӧ�������������ݣ���֯��ʽ���£�
	<types>
	   <type1 colIdx="0,1,2��..." datatype="REALNUMBER,NULL,NULL,..." />
	   <type2 colIdx="-1" datatype="REALNUMBER" />
	   ...
	</types>
����type�����ӽڵ��������Ψһ��ʶ��colIdx������ݵ�������������TextBox�ɲ�д�����������ֱ���Զ��Ÿ�������datatype�����colIdx��Ӧ���������ͣ��������֮����
���Ÿ��������õ�����������NULL����UNLIMITED�����������ƣ���REALNUMBER��ʵ���ͣ���INTEGER�������ͣ���ANGLE���Ƕ��ͣ�


4.tipmessages�ڵ�ָʾ����ť��֤��ͨ��ʱ�Ĵ�����ʾ��Ϣ��errorno����Ϊ�����ţ���������ż��Զ��ŷָ�����0����ʾ��֤�ɹ�����1����ʾ��֤ʧ�ܣ�errormessageָ
ʾ������ʾ��Ϣ�����������Ϣ���ö��ŷָ������ֻ���Զ�����֤ʧ����Ϣ����ֻ����дerrormessage����ֵ����
	<tipmessages>
	   <tip1 errorno="0,1" errormessage="��֤�ɹ�,�ð�ť���ڿ��õı�����" />
	   <tip2 errormessage="�ð�ť���ڿ��õı�����" />
	   ...
	</tipmessages>